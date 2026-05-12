using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public record GetLedgerHeadersQuery(string? Classification = null, string? Nature = null) 
        : IRequest<Result<IEnumerable<LedgerAccountHeaderDto>>>;
}
