using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class RemoveBranchAccessCommandHandler
        : IRequestHandler<RemoveBranchAccessCommand, Result> {
        private readonly IUnitOfWork _uow;
        private readonly IUserContext _userContext;

        public RemoveBranchAccessCommandHandler(IUnitOfWork uow, IUserContext userContext) {
            _uow = uow; _userContext = userContext;
        }

        public async Task<Result> Handle(
            RemoveBranchAccessCommand command, CancellationToken ct) {
            var user = await _uow.Users.GetByIdAsync(command.UserId, ct);
            if (user is null)
                return Result.Failure("User not found.", "NOT_FOUND");

            if (user.DefaultBranchId == command.BranchId)
                return Result.Failure("Cannot remove access to the user's home branch. " +
                    "Change home branch first.", "HOME_BRANCH");

            var access = await _uow.Users.GetBranchAccessEntryAsync(command.UserId, command.BranchId, ct);
            if (access is null || access.IsDeleted)
                return Result.Failure("Branch access record not found.", "NOT_FOUND");

            access.IsDeleted = true;
            access.DeletedOn = DateTime.UtcNow;
            access.DeletedBy = _userContext.Username;
            _uow.Users.UpdateBranchAccess(access);
            await _uow.CommitAsync(ct);
            return Result.Success();
        }
    }

}
