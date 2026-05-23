using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Products {
    public class GetLoanProductByIdQueryHandler(IUnitOfWork uow) : IRequestHandler<GetLoanProductByIdQuery, Result<LoanProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<LoanProductDetailDto>> Handle(GetLoanProductByIdQuery query, CancellationToken ct) {
            var p = await _uow.Products.GetLoanProductByIdAsync(query.Id, ct);
            if (p is null) return Result<LoanProductDetailDto>.NotFound("Loan product not found.");
            return Result<LoanProductDetailDto>.Success(ProductMapper.MapLoanDetail(p));
        }
    }
}
