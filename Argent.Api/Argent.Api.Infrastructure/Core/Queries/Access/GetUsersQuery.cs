using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public record GetUsersQuery(string? Search = null, long? BranchId = null, bool? IsActive=null, int Page = 1, int PageSize = 20) :
        IRequest<Result<PagedResult<UserSummaryDto>>>;
}
