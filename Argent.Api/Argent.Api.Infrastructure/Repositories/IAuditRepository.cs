using Argent.Api.Domain.Entities.Audit;

namespace Argent.Api.Infrastructure.Repositories {
    public interface IAuditRepository {
        Task AddAsync(AuditLog auditLog, CancellationToken ct = default);
        Task<IEnumerable<AuditLog>> GetByUserAsync(long userId, int page, int pageSize, CancellationToken token = default);
        Task<IEnumerable<AuditLog>> GetByBranchAsync(long branchId, DateTime from, DateTime to, CancellationToken token = default);
        Task<IEnumerable<AuditLog>> GetByEntityAsync(string entityName, string entityId, CancellationToken token = default);
    }
}
