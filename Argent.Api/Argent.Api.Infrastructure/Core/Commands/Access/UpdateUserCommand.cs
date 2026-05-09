using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record UpdateUserCommand(long UserId, UpdateUserRequest Request)
         : IRequest<Result<UserDto>>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "UpdateUser";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "AppUser";
    }
}
