using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public class GetUserByIdQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetUserByIdQuery, Result<UserDetailDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<UserDetailDto>> Handle(GetUserByIdQuery query, CancellationToken ct) {
            var user = await _uow.Users.GetWithAccessAsync(query.Id, ct);
            if (user is null)
                return Result<UserDetailDto>.NotFound("User not found.");

            var branchAccess = await _uow.Users.GetBranchAccessAsync(user.Id, ct);

            return Result<UserDetailDto>.Success(new UserDetailDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                FullName = $"{user.FirstName} {user.MiddleName} {user.LastName}".Replace("  ", " ").Trim(),
                Username = user.Username,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                IsActive = user.IsActive,
                LastLoginOn = user.LastLoginOn,
                DefaultBranchId = user.DefaultBranchId,
                DefaultBranchCode = user.DefaultBranch?.BranchCode ?? string.Empty,
                DefaultBranchName = user.DefaultBranch?.BranchName ?? string.Empty,
                CreatedOn = user.CreatedOn,
                Roles = user.UserRoles.Where(ur => !ur.IsDeleted)
                    .Select(ur => ur.Role?.Name ?? string.Empty),
                BranchAccess = branchAccess.Select(ba => new BranchAccessDto
                {
                    BranchId = ba.BranchId,
                    BranchCode = ba.Branch?.BranchCode ?? string.Empty,
                    BranchName = ba.Branch?.BranchName ?? string.Empty,
                    CanPost = ba.CanPost,
                    IsDefault = ba.BranchId == user.DefaultBranchId
                })
            });


        }
    }
}
