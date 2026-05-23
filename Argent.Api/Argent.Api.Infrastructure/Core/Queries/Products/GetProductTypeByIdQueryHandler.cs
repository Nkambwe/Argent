using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetProductTypeByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetProductTypeByIdQuery, Result<ProductTypeDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<ProductTypeDto>> Handle(GetProductTypeByIdQuery query, CancellationToken ct) {
            var type = await _uow.Products.GetProductTypeByIdAsync(query.Id, ct);
            if (type is null) return Result<ProductTypeDto>.NotFound("Product type not found.");
            return Result<ProductTypeDto>.Success(ProductMapper.MapProductType(type));
        }
    }
}
