using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetShareProductByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetShareProductByIdQuery, Result<ShareProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<ShareProductDetailDto>> Handle(GetShareProductByIdQuery query, CancellationToken ct) {
            var p = await _uow.Products.GetShareProductByIdAsync(query.Id, ct);
            if (p is null) return Result<ShareProductDetailDto>.NotFound("Share product not found.");
            return Result<ShareProductDetailDto>.Success(ProductMapper.MapShareDetail(p));
        }
    }
}
