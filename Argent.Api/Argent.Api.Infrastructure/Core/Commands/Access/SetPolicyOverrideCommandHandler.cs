using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Data;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class SetPolicyOverrideCommandHandler
    : IRequestHandler<SetPolicyOverrideCommand, Result<PolicyOverrideDto>> {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public SetPolicyOverrideCommandHandler(IUnitOfWork uow, IUserContext userContext) {
            _uow = uow;
            _userContext = userContext;
        }

        public async Task<Result<PolicyOverrideDto>> Handle(
            SetPolicyOverrideCommand command, CancellationToken ct) {
            var group = await _uow.RoleGroups.GetByIdAsync(command.RoleGroupId, ct);
            if (group is null)
                return Result<PolicyOverrideDto>.NotFound("Role group not found.");

            // Load the system policy from Config repository
            var policy = await _uow.SystemPolicies.GetByIdAsync(command.SystemPolicyId, ct);
            if (policy is null)
                return Result<PolicyOverrideDto>.NotFound("System policy not found.");

            if (!policy.IsOverridable)
                return Result<PolicyOverrideDto>.Failure($"Policy '{policy.Name}' cannot be overridden at group level.", "POLICY_NOT_OVERRIDABLE");

            var existing = await _uow.RoleGroups.GetPolicyOverrideAsync(
                command.RoleGroupId, command.SystemPolicyId, ct);

            if (existing is not null) {
                // Upsert — reactivate and update if previously removed
                existing.IsDeleted = false;
                existing.DeletedOn = null;
                existing.DeletedBy = null;
                existing.OverrideValue = command.OverrideValue;
                existing.Reason = command.Reason;
                existing.UpdatedBy = _userContext.Username;
                _uow.RoleGroups.UpdatePolicyOverride(existing);
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
                await _uow.RoleGroups.AddPolicyOverrideAsync(existing, ct);
            }

            await _uow.CommitAsync(ct);

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
