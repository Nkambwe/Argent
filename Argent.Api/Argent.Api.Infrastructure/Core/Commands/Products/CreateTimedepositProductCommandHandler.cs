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
    public class CreateTimedepositProductCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<CreateTimedepositProductCommand, Result<TimedepositProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<TimedepositProductDetailDto>> Handle(CreateTimedepositProductCommand command, CancellationToken ct) {
            var req = command.Request;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"TIMEDEPOSIT-PRODUCT-{req.Code}";

            if (await _uow.Products.ProductCodeExistsAsync(req.Code, ProductModuleType.TimeDeposit, ct: ct))
                return Result<TimedepositProductDetailDto>.Failure($"Product code '{req.Code}' already exists.", "DUPLICATE_CODE");

            var productType = await _uow.Products.GetProductTypeByIdAsync(req.ProductTypeId, ct);
            if (productType?.Module != ProductModuleType.TimeDeposit)
                return Result<TimedepositProductDetailDto>.Failure("Product type does not belong to the Time Deposit module.", "WRONG_MODULE");

            if (req.TierInterest && req.InterestRates.Count > 0)
                return Result<TimedepositProductDetailDto>.Failure(
                    "Cannot set both interest rates and tier interest on the same product. " +
                    "Use either flat rates (InterestRates) or tier bands (InterestTiers).",
                    "CONFLICTING_RATES");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                var product = new TimedepositProduct
                {
                    Code = req.Code,
                    ProductName = req.ProductName,
                    Description = req.Description,
                    ProductTypeId = req.ProductTypeId,
                    ChargeGroupId = req.ChargeGroupId,
                    VatInclusive = req.VatInclusive,
                    UseChargeGroups = req.UseChargeGroups,
                    WithdrawMode = req.WithdrawMode,
                    CapitalizeInterest = req.CapitalizeInterest,
                    ForfeitInterestForPrematureWithdraw = req.ForfeitInterestForPrematureWithdraw,
                    PrematureWithdrawPenalty = req.PrematureWithdrawPenalty,
                    Period = req.Period,
                    PeriodType = req.PeriodType,
                    MinimumAmount = req.MinimumAmount,
                    MaximumAmount = req.MaximumAmount,
                    TierInterest = req.TierInterest,
                    TierMethod = req.TierMethod,
                    IsActive = true,
                    CreatedBy = _userContext.Username
                };

                await _uow.Products.AddTimedepositProductAsync(product, token);
                await _uow.CommitAsync(token);

                logger.Log($"Timedeposit Product added: {product.Code} — {product.ProductName}", "PDT-OK");

                //..seed default configuration
                product.Configuration = new TimedepositProductConfiguration
                {
                    TimedepositProductId = product.Id,
                    MinimumProductAmount = req.MinimumAmount,
                    MaximumProductAmount = req.MaximumAmount,
                    InterestPeriodInDays = 365,
                    CreatedBy = _userContext.Username
                };
                _uow.Products.UpdateTimedepositProduct(product);

                //..interest rates using flat schedule
                foreach (var r in req.InterestRates)
                    await _uow.Products.AddTimedepositRateAsync(new TimedepositRate
                    {
                        TimedepositProductId = product.Id,
                        Period = r.Period,
                        PeriodType = r.PeriodType,
                        InterestRate = r.InterestRate,
                        MinimumAmount = r.MinimumAmount,
                        IsActive = true,
                        CreatedBy = _userContext.Username
                    }, token);

                //..tnterest tiers, amount-based
                foreach (var t in req.InterestTiers)
                    await _uow.Products.AddTimedepositTierAsync(new TimedepositInterestTier
                    {
                        TimedepositProductId = product.Id,
                        FromAmount = t.FromAmount,
                        ToAmount = t.ToAmount,
                        Rate = t.Rate,
                        IsActive = true,
                        CreatedBy = _userContext.Username
                    }, token);

                //..posting accounts
                foreach (var pa in req.PostingAccounts)
                    await _uow.Products.AddPostingAccountAsync(new ProductPostingAccount
                    {
                        ProductId = product.Id,
                        ProductModule = ProductModuleType.TimeDeposit,
                        PostingPurpose = pa.PostingPurpose,
                        CustomerSegment = pa.CustomerSegment,
                        LedgerNumber = pa.LedgerNumber,
                        CostCentreCode = pa.CostCentreCode,
                        RevenueCentreCode = pa.RevenueCentreCode,
                        CreatedBy = _userContext.Username
                    }, token);

                var created = await _uow.Products.GetTimedepositProductByIdAsync(product.Id, token);
                return Result<TimedepositProductDetailDto>.Success(
                    ProductMapper.MapTimedepositDetail(created!));
            }, ct);
        }
    }
}
