using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class UpdateBranchAccessCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<UpdateBranchAccessCommand, Result> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result> Handle(UpdateBranchAccessCommand command, CancellationToken ct) {
            var access = await _uow.Users.GetBranchAccessEntryAsync(command.UserId, command.BranchId, ct);
            if (access is null || access.IsDeleted)
                return Result.Failure("Branch access record not found.", "NOT_FOUND");

            access.CanPost = command.CanPost;
            access.UpdatedBy = _userContext.Username;
            _uow.Users.UpdateBranchAccess(access);
            await _uow.CommitAsync(ct);
            return Result.Success();
        }
    }

}
