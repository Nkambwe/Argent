using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetTimedepositProductByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetTimedepositProductByIdQuery, Result<TimedepositProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<TimedepositProductDetailDto>> Handle(GetTimedepositProductByIdQuery query, CancellationToken ct) {
            var p = await _uow.Products.GetTimedepositProductByIdAsync(query.Id, ct);
            if (p is null)
                return Result<TimedepositProductDetailDto>.NotFound("Time deposit product not found.");
            return Result<TimedepositProductDetailDto>.Success(ProductMapper.MapTimedepositDetail(p));
        }
    }
}
