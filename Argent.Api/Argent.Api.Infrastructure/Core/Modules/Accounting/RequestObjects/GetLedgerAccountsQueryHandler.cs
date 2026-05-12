using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public class GetLedgerAccountsQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetLedgerAccountsQuery, Result<PagedResult<LedgerAccountDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<PagedResult<LedgerAccountDto>>> Handle(GetLedgerAccountsQuery query, CancellationToken ct) {
            var accounts = await _uow.Accounting.GetLedgerAccountsAsync(ct);

            var unfiltered = accounts.AsQueryable();

            var filters = query.Request;
            if (!string.IsNullOrWhiteSpace(filters.Classification) &&
                Enum.TryParse<AccountClassification>(filters.Classification, out var cls))
                unfiltered = unfiltered.Where(a => a.AccountClassification == cls);

            if (!string.IsNullOrWhiteSpace(filters.Nature) &&
                Enum.TryParse<AccountNature>(filters.Nature, out var nature))
                unfiltered = unfiltered.Where(a => a.AccountNature == nature);

            if (filters.Suspended.HasValue)
                unfiltered = unfiltered.Where(a => a.Suspended == filters.Suspended.Value);

            if (filters.HeaderId.HasValue)
                unfiltered = unfiltered.Where(a => a.LedgerAccountHeaderId == filters.HeaderId.Value);

            var total = unfiltered.Count();
            var items = unfiltered.OrderBy(a => a.LedgerIndex).Skip((filters.Page - 1) * filters.PageSize).Take(filters.PageSize)
                .Select(AccountMapper.MapAccountToDto)
                .ToList();

            return Result<PagedResult<LedgerAccountDto>>.Success(
                new PagedResult<LedgerAccountDto>(items, total, filters.Page, filters.PageSize));
        }
    }

}
