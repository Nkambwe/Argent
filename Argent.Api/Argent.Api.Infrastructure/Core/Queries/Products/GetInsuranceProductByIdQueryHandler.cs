using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetInsuranceProductByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetInsuranceProductByIdQuery, Result<InsuranceProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<InsuranceProductDetailDto>> Handle(GetInsuranceProductByIdQuery query, CancellationToken ct) {
            var p = await _uow.Products.GetInsuranceProductByIdAsync(query.Id, ct);
            if (p is null)
                return Result<InsuranceProductDetailDto>.NotFound("Insurance product not found.");
            return Result<InsuranceProductDetailDto>.Success(ProductMapper.MapInsuranceDetail(p));
        }
    }
}
