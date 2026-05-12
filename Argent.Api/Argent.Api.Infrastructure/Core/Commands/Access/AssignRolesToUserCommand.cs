using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record AssignRolesToUserCommand(long UserId, List<long> RoleIds) 
        : IRequest<Result<UserDetailDto>>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "AssignRolesToUser";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "AppUser";
    }

}
