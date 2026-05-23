using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpdateLoanProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<UpdateLoanProductCommand, Result<LoanProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<LoanProductDetailDto>> Handle(UpdateLoanProductCommand command, CancellationToken ct) {
            var p = await _uow.Products.GetLoanProductByIdAsync(command.Id, ct);
            if (p is null) return Result<LoanProductDetailDto>.NotFound("Loan product not found.");
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"LOAN-PRODUCT-{command.Id}-UPDATE";

            p.ProductName = req.ProductName;
            p.Description = req.Description;
            p.ProductTypeId = req.ProductTypeId;
            p.ChargeGroupId = req.ChargeGroupId;
            p.VatInclusive = req.VatInclusive;
            p.UseChargeGroups = req.UseChargeGroups;
            p.TargetGroup = req.TargetGroup;
            p.UseClasses = req.UseClasses;
            p.UpdatedBy = _userContext.Username;

            _uow.Products.UpdateLoanProduct(p);
            await _uow.CommitAsync(ct);
            logger.Log($"Loan Product updated: {p.Code} — {p.ProductName}", "PDT-OK");

            var updated = await _uow.Products.GetLoanProductByIdAsync(p.Id, ct);
            return Result<LoanProductDetailDto>.Success(ProductMapper.MapLoanDetail(updated!));
        }
    }
}
