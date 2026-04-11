using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {

    public record CreateUserCommand(
        string Username,
        string Email,
        string Password,
        string FirstName,
        string? MiddleName,
        string LastName,
        string? PhoneNumber,
        long DefaultBranchId, List<long> RoleIds) 
    : IRequest<Result<UserDto>>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "CreateUser";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "AppUser";
    }
}
