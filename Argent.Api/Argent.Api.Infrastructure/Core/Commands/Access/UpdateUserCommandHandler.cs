using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class UpdateUserCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory) 
        : IRequestHandler<UpdateUserCommand, Result<UserDto>> {

        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<UserDto>> Handle(UpdateUserCommand command, CancellationToken ct) {

            var logger = _loggerFactory.CreateLogger("access");

            //..retrieve user record
            var user = await _uow.Users.GetWithAccessAsync(command.UserId, ct);
            if (user is null)
                return Result<UserDto>.NotFound("User not found.");

            var req = command.Request;

            logger.Channel = $"UPDATE-USER-{command.UserId}";
            logger.Log($"Updating user with ID : {command.UserId}", "INFO");

            //..check email uniqueness if changing
            if (user.Email != req.Email &&
                await _uow.Users.EmailExistsAsync(req.Email, command.UserId, ct))
                return Result<UserDto>.Failure(
                    $"Email '{req.Email}' is already registered.", "DUPLICATE_EMAIL");

            //..make sure default branch exists
            var defaultBranch = await _uow.Organizations.GetBranchByIdAsync(req.DefualtBranchId, ct);
            if (defaultBranch is null)
                return Result<UserDto>.NotFound("Default branch not found.");

            var result = await _uow.ExecuteInTransactionAsync(async token => {
                user.FirstName = req.FirstName;
                user.MiddleName = req.MiddleName;
                user.LastName = req.LastName;
                user.Email = req.Email;
                user.PhoneNumber = req.PhoneNumber;
                user.IsActive = req.IsActive;
                user.DefaultBranchId = req.DefualtBranchId;

                _uow.Users.Update(user);
                await _uow.CommitAsync(ct);

                return new UserDto
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    MiddleName = user.MiddleName,
                    LastName = user.LastName,
                    Username = user.Username,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    IsActive = user.IsActive,
                    DefaultBranchId = user.DefaultBranchId,
                    DefaultBranchCode = defaultBranch.BranchCode,
                    DefaultBranchName = defaultBranch.BranchName,
                    Roles = user.UserRoles
                        .Where(ur => !ur.IsDeleted)
                        .Select(ur => ur.Role?.Name ?? string.Empty)
                };
            }, ct);

            return Result<UserDto>.Success(result);
        }
    }

}
