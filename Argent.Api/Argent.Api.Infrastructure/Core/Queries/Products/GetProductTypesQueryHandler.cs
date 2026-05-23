using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {

    public class GetProductTypesQueryHandler(IUnitOfWork uow)
        : IRequestHandler<GetProductTypesQuery, Result<IEnumerable<ProductTypeDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<ProductTypeDto>>> Handle(GetProductTypesQuery query, CancellationToken ct) {
            var types = await _uow.Products.GetProductTypesAsync(query.Module, ct);
            return Result<IEnumerable<ProductTypeDto>>.Success(types.Select(t => ProductMapper.MapProductType(t)));
        }
    }
}
