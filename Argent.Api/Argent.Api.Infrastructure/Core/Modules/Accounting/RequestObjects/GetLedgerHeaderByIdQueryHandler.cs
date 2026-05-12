using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public class GetLedgerHeaderByIdQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetLedgerHeaderByIdQuery, Result<LedgerAccountHeaderDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<LedgerAccountHeaderDto>> Handle(GetLedgerHeaderByIdQuery query, CancellationToken ct) {
            var header = await _uow.Accounting.GetHeaderByIdAsync(query.Id, ct);
            if (header is null)
                return Result<LedgerAccountHeaderDto>.NotFound("Ledger header not found.");

            return Result<LedgerAccountHeaderDto>.Success(AccountMapper.MapHeaderToDto(header));
        }
    }

}
