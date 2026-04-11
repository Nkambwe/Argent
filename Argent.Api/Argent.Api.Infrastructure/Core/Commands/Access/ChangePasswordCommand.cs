using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record ChangePasswordCommand(
        long UserId,
        string CurrentPassword,
        string NewPassword
    ) : IRequest<Result>, IAuditableCommand {
            public string AuditModule => "Access";
            public string AuditAction => "ChangePassword";
            public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
            public string? AuditEntityName => "AppUser";
        }
}
