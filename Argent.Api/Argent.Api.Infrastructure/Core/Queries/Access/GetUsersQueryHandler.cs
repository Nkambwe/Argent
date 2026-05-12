using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public class GetUsersQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetUsersQuery, Result<PagedResult<UserSummaryDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<PagedResult<UserSummaryDto>>> Handle(GetUsersQuery query, CancellationToken ct) {
            var users = await _uow.Users.GetAllWithBranchAsync(ct);

            // Apply filters
            var filtered = users.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Search))
                filtered = filtered.Where(u =>
                    u.Username.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                    u.FirstName.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                    u.LastName.Contains(query.Search, StringComparison.OrdinalIgnoreCase) ||
                    u.Email.Contains(query.Search, StringComparison.OrdinalIgnoreCase));

            if (query.BranchId.HasValue)
                filtered = filtered.Where(u => u.DefaultBranchId == query.BranchId.Value ||
                    u.BranchAccess.Any(ba => ba.BranchId == query.BranchId.Value && !ba.IsDeleted));

            if (query.IsActive.HasValue)
                filtered = filtered.Where(u => u.IsActive == query.IsActive.Value);

            var total = filtered.Count();
            var items = filtered
                .Skip((query.Page - 1) * query.PageSize)
                .Take(query.PageSize)
                .Select(u => new UserSummaryDto
                {
                    Id = u.Id,
                    FullName = $"{u.FirstName} {u.MiddleName} {u.LastName}".Replace("  ", " ").Trim(),
                    Username = u.Username,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    IsActive = u.IsActive,
                    LastLoginOn = u.LastLoginOn,
                    DefaultBranchName = u.DefaultBranch.BranchName ?? string.Empty,
                    Roles = u.UserRoles.Where(ur => !ur.IsDeleted)
                        .Select(ur => ur.Role.Name ?? string.Empty)
                }).ToList();

            return Result<PagedResult<UserSummaryDto>>.Success(
                new PagedResult<UserSummaryDto>(items, total, query.Page, query.PageSize));

        }
    }

}
