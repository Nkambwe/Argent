using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class CreateUserCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory) 
        : IRequestHandler<CreateUserCommand, Result<UserDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<UserDto>> Handle(CreateUserCommand command, CancellationToken ct) {
            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = $"CREATE-USER-{command.Username}";

            if (await _uow.Access.UsernameExistsAsync(command.Username, ct))
                return Result<UserDto>.Failure($"Username '{command.Username}' is already taken.", "DUPLICATE_USERNAME");

            if (await _uow.Access.EmailExistsAsync(command.Email, ct))
                return Result<UserDto>.Failure($"Email '{command.Email}' is already registered.", "DUPLICATE_EMAIL");

            // Verify home branch exists
            var branch = await _uow.Organizations.GetBranchByIdAsync(command.DefaultBranchId, ct);
            if (branch is null)
                return Result<UserDto>.NotFound("Home branch not found.");

            await _uow.BeginTransactionAsync(ct);
            try {
                var user = new AppUser
                {
                    Username = command.Username,
                    Email = command.Email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword(command.Password, workFactor: 12),
                    FirstName = command.FirstName,
                    LastName = command.LastName,
                    PhoneNumber = command.PhoneNumber,
                    DefaultBranchId = command.DefaultBranchId,
                    IsActive = true
                };

                await _uow.Access.AddUserAsync(user, ct);
                await _uow.CommitAsync(ct); // flush to get user.Id

                // Assign roles
                foreach (var roleId in command.RoleIds)
                    await _uow.Access.AssignRoleToUserAsync(user.Id, roleId, ct);

                // Grant access to home branch by default
                await _uow.Access.AssignBranchAccessAsync(user.Id, command.DefaultBranchId, canPost: true, ct);

                await _uow.CommitAsync(ct);

                logger.Log($"User created: {user.Username} (Id: {user.Id})", "INFO");

                return Result<UserDto>.Success(new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    DefaultBranchId = user.DefaultBranchId,
                    HomeBranchName = branch.BranchName,
                    IsActive = user.IsActive
                });
            } catch {
                await _uow.RollbackAsync(ct);
                throw;
            }
        }
    }
}
