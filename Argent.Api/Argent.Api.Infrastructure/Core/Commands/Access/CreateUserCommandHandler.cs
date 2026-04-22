using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class CreateUserCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory) : IRequestHandler<CreateUserCommand, Result<UserDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<UserDto>> Handle(CreateUserCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = $"CREATE-USER-{command.Username}";
            logger.Log($"Creating user: {command.Username}", "INFO");

            if (await _uow.Access.UsernameExistsAsync(command.Username, ct))
                return Result<UserDto>.Failure($"Username '{command.Username}' is already taken.", "DUPLICATE_USERNAME");

            if (await _uow.Access.EmailExistsAsync(command.Email, ct))
                return Result<UserDto>.Failure($"Email '{command.Email}' is already registered.", "DUPLICATE_EMAIL");

            // Verify home branch exists
            var branch = await _uow.Organizations.GetBranchByIdAsync(command.DefaultBranchId, ct);
            if (branch is null)
                return Result<UserDto>.NotFound("Default branch not found.");

            //..validate all roles exist before starting transaction
            foreach (var roleId in command.RoleIds) {
                var role = await _uow.Access.GetRoleByIdAsync(roleId, ct);
                if (role is null)
                    return Result<UserDto>.NotFound($"Role {roleId} not found.");
            }

            var result = await _uow.ExecuteInTransactionAsync(async token => {
                // ..create user
                var user = new AppUser
                {
                    Username = command.Username,
                    Email = command.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password, workFactor: 12),
                    FirstName = command.FirstName,
                    MiddleName = command.MiddleName,
                    LastName = command.LastName,
                    PhoneNumber = command.PhoneNumber,
                    DefaultBranchId = command.DefaultBranchId,
                    IsActive = true
                };

                await _uow.Access.AddUserAsync(user, token);
                //..first SaveChanges to get user.Id
                await _uow.CommitAuditAsync(token); 

                //..assign roles
                foreach (var roleId in command.RoleIds)
                    await _uow.Access.AssignRoleToUserAsync(user.Id, roleId, token);

                //..default branch access is always granted
                await _uow.Access.AssignBranchAccessAsync(user.Id, command.DefaultBranchId, canPost: true, token);
                //..second SaveChanges for roles and branch access
                await _uow.CommitAuditAsync(token); 

                logger.Log($"User created: {user.Username} (Id: {user.Id})", "INFO");

                //..reload roles for response
                var roles = await _uow.Access.GetAllRolesAsync(token);
                var assignedRoleNames = roles.Where(r => command.RoleIds.Contains(r.Id)).Select(r => r.Name);

                return new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    DefaultBranchId = user.DefaultBranchId,
                    DefaultBranchCode = branch?.BranchCode ?? "",
                    DefaultBranchName = branch?.BranchName ?? "",
                    IsActive = user.IsActive,
                    CreatedOn = user.CreatedOn,
                    Roles = assignedRoleNames,
                    BranchAccess = [new BranchAccessDto
                    {
                        BranchId   = branch?.Id ?? 1,
                        BranchCode = branch?.BranchCode ?? "",
                        BranchName = branch?.BranchName ?? "",
                        CanPost    = true
                    }]
                };
            }, ct);

            return Result<UserDto>.Success(result);

        }
    }
}
