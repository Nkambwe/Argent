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

            var filtered = accounts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Classification) &&
                Enum.TryParse<AccountClassification>(query.Classification, out var cls))
                filtered = filtered.Where(a => a.AccountClassification == cls);

            if (!string.IsNullOrWhiteSpace(query.Nature) &&
                Enum.TryParse<AccountNature>(query.Nature, out var nature))
                filtered = filtered.Where(a => a.AccountNature == nature);

            if (query.Suspended.HasValue)
                filtered = filtered.Where(a => a.Suspended == query.Suspended.Value);

            if (query.HeaderId.HasValue)
                filtered = filtered.Where(a => a.LedgerAccountHeaderId == query.HeaderId.Value);

            var total = filtered.Count();
            var items = filtered.OrderBy(a => a.LedgerIndex).Skip((query.Page - 1) * query.PageSize).Take(query.PageSize)
                .Select(AccountMapper.MapAccountToDto)
                .ToList();

            return Result<PagedResult<LedgerAccountDto>>.Success(
                new PagedResult<LedgerAccountDto>(items, total, query.Page, query.PageSize));
        }
    }

}
