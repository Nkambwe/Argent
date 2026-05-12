using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record CreateRoleCommand(string Name, string? Description, List<long> PermissionIds) 
        : IRequest<Result<RoleDto>>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "CreateRole";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "Role";
    }

}
