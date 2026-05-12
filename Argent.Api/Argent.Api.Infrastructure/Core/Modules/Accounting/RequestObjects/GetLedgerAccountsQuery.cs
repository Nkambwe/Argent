using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public record GetLedgerAccountsQuery(string? Classification = null, string? Nature = null,
        bool? Suspended = null, long? HeaderId = null, int Page = 1, int PageSize = 50)
        : IRequest<Result<PagedResult<LedgerAccountDto>>>;
}
