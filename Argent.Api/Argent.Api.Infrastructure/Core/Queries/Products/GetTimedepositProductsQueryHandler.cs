using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetTimedepositProductsQueryHandler(IUnitOfWork uow) : IRequestHandler<GetTimedepositProductsQuery, Result<IEnumerable<TimedepositProductSummaryDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<TimedepositProductSummaryDto>>> Handle(GetTimedepositProductsQuery query, CancellationToken ct) {
            var products = await _uow.Products.GetTimedepositProductsAsync(query.IsActive, ct);
            return Result<IEnumerable<TimedepositProductSummaryDto>>.Success(products.Select(p => ProductMapper.MapTimedepositSummary(p)));
        }
    }
}
