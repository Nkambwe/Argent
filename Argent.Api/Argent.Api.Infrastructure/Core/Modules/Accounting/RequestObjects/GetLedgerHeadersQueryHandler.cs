using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public class GetLedgerHeadersQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetLedgerHeadersQuery, Result<IEnumerable<LedgerAccountHeaderDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<LedgerAccountHeaderDto>>> Handle(GetLedgerHeadersQuery query, CancellationToken ct) {
            var headers = await _uow.Accounting.GetHeadersAsync(ct);

            var filtered = headers.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Classification) &&
                Enum.TryParse<AccountClassification>(query.Classification, out var cls))
                filtered = filtered.Where(h => h.AccountClassification == cls);

            if (!string.IsNullOrWhiteSpace(query.Nature) &&
                Enum.TryParse<AccountNature>(query.Nature, out var nature))
                filtered = filtered.Where(h => h.AccountNature == nature);

            //..only return creatable header types — hide Label, Category, and total rows
            filtered = filtered.Where(h =>
                h.AccountCategory == AccountCategory.SubCategory ||
                h.AccountCategory == AccountCategory.Header);

            return Result<IEnumerable<LedgerAccountHeaderDto>>.Success(filtered.OrderBy(h => h.LedgerIndex).Select(AccountMapper.MapHeaderToDto));
        }
    }

}
