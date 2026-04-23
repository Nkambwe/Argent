using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    // ── Remove Policy Override ─────────────────────────────────────────────────

    public record RemovePolicyOverrideCommand(long RoleGroupId, long SystemPolicyId) 
        : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "RemovePolicyOverride";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Delete;
        public string? AuditEntityName => "RoleGroupPolicyOverride";
    }

}
