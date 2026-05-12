using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class DeactivateUserCommandHandler
        : IRequestHandler<DeactivateUserCommand, Result> {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public DeactivateUserCommandHandler(IUnitOfWork uow, IUserContext userContext) {
            _uow = uow; _userContext = userContext;
        }

        public async Task<Result> Handle(
            DeactivateUserCommand command, CancellationToken ct) {
            var user = await _uow.Users.GetByIdAsync(command.UserId, ct);
            if (user is null)
                return Result.Failure("User not found.", "NOT_FOUND");

            if (user.Username == "admin")
                return Result.Failure("The system admin account cannot be deactivated.", "PROTECTED");

            if (_userContext.UserId == command.UserId)
                return Result.Failure( "You cannot deactivate your own account.", "SELF_DEACTIVATE");

            user.IsActive = false;
            user.IsDeleted = true;
            user.DeletedOn = DateTime.UtcNow;
            user.DeletedBy = _userContext.Username;
            _uow.Users.Update(user);
            await _uow.CommitAsync(ct);
            return Result.Success();
        }
    }


}
