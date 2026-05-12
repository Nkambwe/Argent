using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public class GetAccountsChartQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetAccountsChartQuery, Result<IEnumerable<AccountsChartDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<AccountsChartDto>>> Handle(GetAccountsChartQuery query, CancellationToken ct) {
            var charts = await _uow.Accounting.GetChartsAsync(ct);
            return Result<IEnumerable<AccountsChartDto>>.Success(
                charts.Select(c => new AccountsChartDto
                {
                    Id = c.Id,
                    ChartName = c.ChartName,
                    Description = c.Description,
                    ChartType = c.ChartType.ToString(),
                    HeaderCount = c.LedgerAccounts.Select(a => a.LedgerAccountHeaderId).Distinct().Count(),
                    AccountCount = c.LedgerAccounts.Count(a => !a.IsDeleted)
                }));
        }
    }
}
