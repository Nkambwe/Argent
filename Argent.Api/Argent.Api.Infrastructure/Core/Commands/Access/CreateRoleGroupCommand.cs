using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record CreateRoleGroupCommand(string Name, string? Description, List<long> RoleIds)
        : IRequest<Result<RoleGroupDto>>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "CreateRoleGroup";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "RoleGroup";
    }
}
