using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using Argent.Api.Infrastructure.Helpers;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Products {
    public class UpdateInsuranceProductConfigCommandHandler(IUnitOfWork uow, IUserContext userContext, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<UpdateInsuranceProductConfigCommand, Result<InsuranceProductDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<InsuranceProductDetailDto>> Handle(UpdateInsuranceProductConfigCommand command, CancellationToken ct) {
            var p = await _uow.Products.GetInsuranceProductByIdAsync(command.Id, ct);
            if (p is null)
                return Result<InsuranceProductDetailDto>.NotFound("Insurance product not found.");

            if (p.Configuration is null)
                return Result<InsuranceProductDetailDto>.Failure("No configuration found.", "NO_CONFIG");

            var req = command.Request;
            var cfg = p.Configuration;
            var logger = _loggerFactory.CreateLogger("products");
            logger.Channel = $"INSURANCE-PRODUCT-{p.Code}";

            cfg.PolicyPeriod = req.PolicyPeriod;
            cfg.MinimumNumberInsured = req.MinimumNumberInsured;
            cfg.MaximumNumberInsured = req.MaximumNumberInsured;
            cfg.MinimumInsurableAge = req.MinimumInsurableAge;
            cfg.MaximumInsurableAge = req.MaximumInsurableAge;
            cfg.MonthlyPremium = req.MonthlyPremium;
            cfg.PremiumPercentageCharged = req.PremiumPercentageCharged;
            cfg.ChargeFixedAmount = req.ChargeFixedAmount;
            cfg.FixedAmount = req.FixedAmount;
            cfg.CanModifyPremium = req.CanModifyPremium;
            cfg.MinimumCoverage = req.MinimumCoverage;
            cfg.MaximumCoverage = req.MaximumCoverage;
            cfg.Discount = req.Discount;
            cfg.ClaimsPercentage = req.ClaimsPercentage;
            cfg.AdministrationCostPercentage = req.AdministrationCostPercentage;
            cfg.AdministrationFundPercentage = req.AdministrationFundPercentage;
            cfg.ChargeWithholdingTaxOnCharges = req.ChargeWithholdingTaxOnCharges;
            cfg.ChargeStampDutyOnPolicies = req.ChargeStampDutyOnPolicies;
            cfg.RequireApprovalForClaims = req.RequireApprovalForClaims;
            cfg.WaitingPeriodDays = req.WaitingPeriodDays;
            cfg.UpdatedBy = _userContext.Username;

            _uow.Products.UpdateInsuranceProduct(p);
            await _uow.CommitAsync(ct);

            logger.Log($"Insurance Product Configurations updated: {p.Code} — {p.ProductName}", "PDT-OK");

            var updated = await _uow.Products.GetInsuranceProductByIdAsync(p.Id, ct);
            return Result<InsuranceProductDetailDto>.Success(ProductMapper.MapInsuranceDetail(updated!));
        }
    }

}
