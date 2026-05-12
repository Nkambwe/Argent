using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public class GetLedgerAccountByIdQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetLedgerAccountByIdQuery, Result<LedgerAccountDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<LedgerAccountDto>> Handle(
            GetLedgerAccountByIdQuery query, CancellationToken ct) {
            var account = await _uow.Accounting.GetLedgerAccountByIdAsync(query.Id, ct);
            if (account is null)
                return Result<LedgerAccountDto>.NotFound("Ledger account not found.");

            return Result<LedgerAccountDto>.Success(AccountMapper.MapAccountToDto(account));
        }
    }

}
