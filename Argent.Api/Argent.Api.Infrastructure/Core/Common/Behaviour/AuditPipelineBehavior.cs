using Argent.Api.Domain.Entities.Audit;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;
using System.Diagnostics;
using System.Text.Json;

namespace Argent.Api.Infrastructure.Core.Common.Behaviour {
    /// <summary>
    /// Automatically creates an AuditLog record for every command that implements IAuditableCommand.
    /// Runs AFTER BranchPolicyBehavior and ValidationPipelineBehavior so only
    /// permitted, valid commands are audited.
    ///
    /// The audit record is written in a separate SaveChanges call so a business
    /// rollback does not suppress the audit trail — you always know what was attempted.
    /// </summary>
    public class AuditPipelineBehavior<TRequest, TResponse>( IUserContext userContext, IUnitOfWork uow, IServiceLoggerFactory loggerFactory) : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull {
        private readonly IUserContext _userContext = userContext;
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken ct) {
            if (request is not IAuditableCommand auditableCommand)
                return await next();

            var logger = _loggerFactory.CreateLogger("audit");
            logger.Channel = $"AUDIT-{typeof(TRequest).Name}";

            var audit = new AuditLog
            {
                UserId = _userContext.IsAuthenticated ? _userContext.UserId : null,
                Username = _userContext.IsAuthenticated ? _userContext.Username : "anonymous",
                BranchId = _userContext.IsAuthenticated ? _userContext.CurrentBranchId : null,
                IpAddress = _userContext.IpAddress,
                Module = auditableCommand.AuditModule,
                Action = auditableCommand.AuditAction,
                EntityName = auditableCommand.AuditEntityName,
                ActionType = auditableCommand.AuditActionType,
                OccurredOn = DateTime.UtcNow,
                OldValues = TrySerialize(request)
            };

            var sw = Stopwatch.StartNew();
            bool succeeded = false;
            string? failureReason = null;

            try {
                var response = await next();
                sw.Stop();
                succeeded = true;

                // Capture new state from Result<T>.Data if available
                audit.NewValues = TrySerializeResultData(response);

                // Try to extract entity ID from result
                audit.EntityId = TryExtractEntityId(response);

                return response;
            } catch (Exception ex) {
                sw.Stop();
                failureReason = ex.Message;
                logger.Log($"Command failed: {ex.Message}", "AUDIT-ERROR");
                throw;
            } finally {
                audit.Succeeded = succeeded;
                audit.FailureReason = failureReason;
                audit.DurationMs = sw.ElapsedMilliseconds;

                try {
                    // Write audit in its own operation — independent of any business transaction rollback
                    await _uow.Audits.AddAsync(audit, ct);
                    await _uow.CommitAuditAsync(ct);

                    logger.Log(
                        $"Audit written: {audit.Module}.{audit.Action} | User: {audit.Username} | " +
                        $"Branch: {audit.BranchId} | Succeeded: {audit.Succeeded} | {audit.DurationMs}ms",
                        "AUDIT");
                } catch (Exception auditEx) {
                    //..never let audit failure break the business operation
                    logger.Log($"Failed to write audit log: {auditEx.Message}", "AUDIT-ERROR");
                }
            }
        }

        private static string? TrySerialize(object? obj) {
            if (obj is null) return null;
            try { return JsonSerializer.Serialize(obj, _jsonOptions); }
            catch { return null; }
        }

        private static string? TrySerializeResultData<TResponse>(TResponse response) {
            if (response is null) return null;
            try {
                var dataProp = typeof(TResponse).GetProperty("Data");
                var data = dataProp?.GetValue(response);
                return data is null ? null : JsonSerializer.Serialize(data, _jsonOptions);
            }
            catch { return null; }
        }

        private static string? TryExtractEntityId<TResponse>(TResponse response) {
            if (response is null) return null;
            try {
                var dataProp = typeof(TResponse).GetProperty("Data");
                var data = dataProp?.GetValue(response);
                if (data is null) return null;
                var idProp = data.GetType().GetProperty("Id");
                return idProp?.GetValue(data)?.ToString();
            }
            catch { return null; }
        }

        private static readonly JsonSerializerOptions _jsonOptions = new()
        {
            WriteIndented = false,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

}
