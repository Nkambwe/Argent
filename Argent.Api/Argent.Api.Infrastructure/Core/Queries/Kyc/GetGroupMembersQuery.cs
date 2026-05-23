using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Kyc {
    public record GetGroupMembersQuery(long GroupId, bool? Active = null, int Page = 1, int PageSize = 20) : IRequest<Result<PagedResult<CustomerSummaryDto>>>;

}
