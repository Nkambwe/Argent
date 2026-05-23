using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetSavingProductByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetSavingProductByIdQuery, Result<SavingProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<SavingProductDetailDto>> Handle(GetSavingProductByIdQuery query, CancellationToken ct) {
            var p = await _uow.Products.GetSavingProductByIdAsync(query.Id, ct);
            if (p is null) return Result<SavingProductDetailDto>.NotFound("Saving product not found.");
            return Result<SavingProductDetailDto>.Success(ProductMapper.MapSavingDetail(p));
        }
    }
}
