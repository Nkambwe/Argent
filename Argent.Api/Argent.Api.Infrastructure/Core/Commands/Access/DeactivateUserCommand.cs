using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record DeactivateUserCommand(long UserId)
        : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "DeactivateUser";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Delete;
        public string? AuditEntityName => "AppUser";
    }


}
