using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record AssignPermissionsToRoleCommand(long RoleId, List<long> PermissionIds)
        : IRequest<Result<RoleDto>>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "AssignPermissions";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "Role";
    }

}
