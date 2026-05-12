using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class AssignBranchAccessCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<AssignBranchAccessCommand, Result<UserDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<UserDetailDto>> Handle(AssignBranchAccessCommand command, CancellationToken ct) {
            var user = await _uow.Users.GetByIdAsync(command.UserId, ct);
            if (user is null)
                return Result<UserDetailDto>.NotFound("User not found.");

            var existing = await _uow.Users.GetBranchAccessEntryAsync(command.UserId, command.BranchId, ct);
            if (existing is not null && !existing.IsDeleted)
                return Result<UserDetailDto>.Failure("User already has access to this branch. Use update to change CanPost.", "DUPLICATE");

            if (existing is not null && existing.IsDeleted) {
                existing.IsDeleted = false;
                existing.DeletedOn = null;
                existing.CanPost = command.CanPost;
                existing.UpdatedBy = _userContext.Username;
                _uow.Users.UpdateBranchAccess(existing);
            } else {
                await _uow.Users.AddBranchAccessAsync(new UserBranchAccess {
                    UserId = command.UserId,
                    BranchId = command.BranchId,
                    CanPost = command.CanPost,
                    CreatedBy = _userContext.Username
                }, ct);
            }

            await _uow.CommitAsync(ct);
            var updated = await _uow.Users.GetWithAccessAsync(command.UserId, ct);
            return Result<UserDetailDto>.Success(AccessMapper.MapUserToDetailDto(updated!));
        }
    }

}
