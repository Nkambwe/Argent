using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class SetPolicyOverrideCommandHandler(AppDataContext db, IUserContext userContext)
                : IRequestHandler<SetPolicyOverrideCommand, Result<PolicyOverrideDto>> {
        private readonly AppDataContext _db = db;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<PolicyOverrideDto>> Handle(
            SetPolicyOverrideCommand command, CancellationToken ct) {
            var group = await _db.RoleGroups
                .FirstOrDefaultAsync(g => g.Id == command.RoleGroupId && !g.IsDeleted, ct);
            if (group is null)
                return Result<PolicyOverrideDto>.NotFound("Role group not found.");

            var policy = await _db.SystemPolicies
                .FirstOrDefaultAsync(p => p.Id == command.SystemPolicyId && !p.IsDeleted, ct);
            if (policy is null)
                return Result<PolicyOverrideDto>.NotFound("System policy not found.");

            if (!policy.IsOverridable)
                return Result<PolicyOverrideDto>.Failure(
                    $"Policy '{policy.Name}' cannot be overridden at group level.", "POLICY_NOT_OVERRIDABLE");

            // Upsert: update existing or create new override
            var existing = await _db.RoleGroupPolicyOverrides
                .FirstOrDefaultAsync(o =>
                    o.RoleGroupId == command.RoleGroupId &&
                    o.SystemPolicyId == command.SystemPolicyId, ct);

            if (existing is not null) {
                // Reactivate if soft-deleted
                existing.IsDeleted = false;
                existing.DeletedOn = null;
                existing.DeletedBy = null;
                existing.OverrideValue = command.OverrideValue;
                existing.Reason = command.Reason;
                existing.UpdatedOn = DateTime.UtcNow;
                existing.UpdatedBy = _userContext.Username;
            }
            else {
                existing = new RoleGroupPolicyOverride
                {
                    RoleGroupId = command.RoleGroupId,
                    SystemPolicyId = command.SystemPolicyId,
                    OverrideValue = command.OverrideValue,
                    Reason = command.Reason,
                    CreatedBy = _userContext.Username
                };
                await _db.RoleGroupPolicyOverrides.AddAsync(existing, ct);
            }

            await _db.SaveChangesAsync(ct);

            return Result<PolicyOverrideDto>.Success(new PolicyOverrideDto
            {
                Id = existing.Id,
                SystemPolicyId = existing.SystemPolicyId,
                PolicyName = policy.Name,
                PolicyDescription = policy.Description,
                OverrideValue = existing.OverrideValue,
                Reason = existing.Reason
            });
        }
    }

}
