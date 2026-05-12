using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    public class DeactivateBranchCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<DeactivateBranchCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(
            DeactivateBranchCommand command, CancellationToken ct) {
            var branch = await _uow.Organizations.GetBranchByIdAsync(command.BranchId, ct);
            if (branch is null)
                return Result.Failure("Branch not found.", "NOT_FOUND");

            if (branch.IsDefault)
                return Result.Failure(
                    "Cannot deactivate the default branch. Set another branch as default first.",
                    "DEFAULT_BRANCH");

            branch.IsActive = false;
            branch.IsDeleted = true;
            branch.DeletedOn = DateTime.UtcNow;
            branch.DeletedBy = _userContext.Username;
            _uow.Organizations.UpdateBranch(branch);
            await _uow.CommitAsync(ct);

            return Result.Success();
        }
    }
}
