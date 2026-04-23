using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record UpdateRoleGroupCommand(long RoleGroupId, string Name, string? Description,bool IsActive) 
        : IRequest<Result<RoleGroupDto>>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "UpdateRoleGroup";
        public AuditAction AuditActionType => Domain.Enums .AuditAction.Update;
        public string? AuditEntityName => "RoleGroup";
    }

}
