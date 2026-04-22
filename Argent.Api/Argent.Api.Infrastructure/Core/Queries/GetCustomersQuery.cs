using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    public record GetCustomersQuery(CustomerType CustomerType, long? BranchId, bool? Active, bool? Approved, string? Search, int Page = 1, int PageSize = 20) :
        IRequest<Result<PagedResult<CustomerSummaryDto>>>;

}
