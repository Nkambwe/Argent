using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetLoanProductsQueryHandler(IUnitOfWork uow) : IRequestHandler<GetLoanProductsQuery, Result<IEnumerable<LoanProductSummaryDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<LoanProductSummaryDto>>> Handle(GetLoanProductsQuery query, CancellationToken ct) {
            var products = await _uow.Products.GetLoanProductsAsync(query.IsActive, ct);
            return Result<IEnumerable<LoanProductSummaryDto>>.Success(products.Select(p => ProductMapper.MapLoanSummary(p)));
        }
    }
}
