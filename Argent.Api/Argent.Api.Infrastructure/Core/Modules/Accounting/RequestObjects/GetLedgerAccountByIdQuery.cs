using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public record GetLedgerAccountByIdQuery(long Id)
        : IRequest<Result<LedgerAccountDto>>;
}
