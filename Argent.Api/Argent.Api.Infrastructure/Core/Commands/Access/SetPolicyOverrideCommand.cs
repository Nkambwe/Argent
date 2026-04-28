using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {

    public record SetPolicyOverrideCommand(long RoleGroupId,long SystemPolicyId, string OverrideValue, string? Reason) 
        : IRequest<Result<PolicyOverrideDto>>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "SetPolicyOverride";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "RoleGroupPolicyOverride";
    }

}
