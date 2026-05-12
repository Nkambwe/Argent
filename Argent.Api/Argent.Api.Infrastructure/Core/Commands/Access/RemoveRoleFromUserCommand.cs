using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record RemoveRoleFromUserCommand(long UserId, long RoleId) 
        : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "RemoveRoleFromUser";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "AppUser";
    }


}
