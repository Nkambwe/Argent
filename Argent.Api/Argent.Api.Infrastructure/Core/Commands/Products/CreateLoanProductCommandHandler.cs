using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class CreateLoanProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<CreateLoanProductCommand, Result<LoanProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<LoanProductDetailDto>> Handle(CreateLoanProductCommand command, CancellationToken ct) {
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"LOAN-PRODUCT-{req.Code}-CREATE";

            if (await _uow.Products.ProductCodeExistsAsync(req.Code, ProductModuleType.Loan, ct: ct))
                return Result<LoanProductDetailDto>.Failure($"Product code '{req.Code}' already exists.", "DUPLICATE_CODE");

            var productType = await _uow.Products.GetProductTypeByIdAsync(req.ProductTypeId, ct);
            if (productType is null)
                return Result<LoanProductDetailDto>.NotFound("Product type not found.");
            if (productType.Module != ProductModuleType.Loan)
                return Result<LoanProductDetailDto>.Failure("Product type does not belong to the Loan module.", "WRONG_MODULE");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                var product = new LoanProduct
                {
                    Code = req.Code,
                    ProductName = req.ProductName,
                    Description = req.Description,
                    ProductTypeId = req.ProductTypeId,
                    ChargeGroupId = req.ChargeGroupId,
                    VatInclusive = req.VatInclusive,
                    UseChargeGroups = req.UseChargeGroups,
                    TargetGroup = req.TargetGroup,
                    UseClasses = req.UseClasses,
                    IsActive = true,
                    CreatedBy = _userContext.Username
                };

                await _uow.Products.AddLoanProductAsync(product, token);
                await _uow.CommitAsync(token);

                logger.Log($"Loan Product Registered: {product.Code} — {product.ProductName}", "PDT-OK");

                // Seed default configuration
                var config = new LoanProductConfiguration
                {
                    LoanProductId = product.Id,
                    InterestDays = 365,
                    InterestWeeks = 52,
                    LoanApprovalStages = TierApproval.Tier1,
                    PenaltyCalculationType = PenaltyCalculationType.None,
                    PenaltyCalculationMethod = PenaltyCalculationMethod.None,
                    LastPenaltyCalculationDate = DateTime.UtcNow,
                    GroupSmsSendingOption = GroupSms.All,
                    CreatedBy = _userContext.Username
                };
                product.Configuration = config;
                _uow.Products.UpdateLoanProduct(product);

                foreach (var pa in req.PostingAccounts)
                    await _uow.Products.AddPostingAccountAsync(new ProductPostingAccount
                    {
                        ProductId = product.Id,
                        ProductModule = ProductModuleType.Loan,
                        PostingPurpose = pa.PostingPurpose,
                        CustomerSegment = pa.CustomerSegment,
                        LedgerNumber = pa.LedgerNumber,
                        CostCentreCode = pa.CostCentreCode,
                        RevenueCentreCode = pa.RevenueCentreCode,
                        CreatedBy = _userContext.Username
                    }, token);

                var created = await _uow.Products.GetLoanProductByIdAsync(product.Id, token);
                return Result<LoanProductDetailDto>.Success(ProductMapper.MapLoanDetail(created!));
            }, ct);
        }
    }
}
