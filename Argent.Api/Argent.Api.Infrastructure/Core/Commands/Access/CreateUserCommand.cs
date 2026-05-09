using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Argent.Api.Infrastructure.Core.Commands.Access {

    public record CreateUserCommand(CreateUserRequest Request) 
    : IRequest<Result<UserDto>>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "CreateUser";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "AppUser";
    }

}
