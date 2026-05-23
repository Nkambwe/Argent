using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetShareProductsQueryHandler(IUnitOfWork uow) : IRequestHandler<GetShareProductsQuery, Result<IEnumerable<ShareProductSummaryDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<ShareProductSummaryDto>>> Handle(GetShareProductsQuery query, CancellationToken ct) {
            var products = await _uow.Products.GetShareProductsAsync(query.IsActive, ct);
            return Result<IEnumerable<ShareProductSummaryDto>>.Success(products.Select(p => ProductMapper.MapShareSummary(p)));
        }
    }
}
