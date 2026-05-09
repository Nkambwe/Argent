using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record ChangePasswordCommand(long UserId, ChangePasswordRequest Request) 
        : IRequest<Result>, IAuditableCommand {
            public string AuditModule => "Access";
            public string AuditAction => "ChangePassword";
            public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
            public string? AuditEntityName => "AppUser";
        }
}
