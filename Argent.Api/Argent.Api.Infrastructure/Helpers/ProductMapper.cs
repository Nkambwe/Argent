using Argent.Api.Domain.Entities.Products;
using Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Argent.Api.Infrastructure.Helpers {
    public static class ProductMapper {
        public static ProductTypeDto MapProductType(ProductType t) => new()
        {
            Id = t.Id,
            Code = t.Code,
            Name = t.Name,
            Module = t.Module.ToString(),
            Series = t.Series,
            IsActive = t.IsActive,
            IsSystem = t.IsSystem,
            Description = t.Description
        };

        public static PostingAccountDto MapPostingAccount(ProductPostingAccount a) => new()
        {
            Id = a.Id,
            ProductId = a.ProductId,
            ProductModule = a.ProductModule.ToString(),
            PostingPurpose = a.PostingPurpose.ToString(),
            CustomerSegment = a.CustomerSegment.ToString(),
            LedgerNumber = a.LedgerNumber,
            CostCentreCode = a.CostCentreCode,
            RevenueCentreCode = a.RevenueCentreCode
        };

        public static ProductParamDto MapParam(ProductParam p) => new()
        {
            Id = p.Id,
            ParameterName = p.ParameterName,
            ParamValue = p.ParamValue,
            DataType = p.DataType,
            Description = p.Description,
            IsEditable = p.IsEditable
        };

        public static SavingProductSummaryDto MapSavingSummary(SavingProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            IsActive = p.IsActive,
            OfferInterest = p.OfferInterest,
            InterestRate = p.InterestRate,
            AllowOverdraft = p.AllowOverdraft,
            MinimumBalance = p.MinimumBalance
        };

        public static SavingProductDetailDto MapSavingDetail(SavingProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            IsActive = p.IsActive,
            OfferInterest = p.OfferInterest,
            InterestRate = p.InterestRate,
            AllowOverdraft = p.AllowOverdraft,
            MinimumBalance = p.MinimumBalance,
            LimitWithdraw = p.LimitWithdraw,
            MaximumWithdraws = p.MaximumWithdraws,
            WithdrawPenalty = p.WithdrawPenalty,
            ChargeWithdraws = p.ChargeWithdraws,
            OverdraftInterest = p.OverdraftInterest,
            MinimumInterestOffered = p.MinimumInterestOffered,
            VatInclusive = p.VatInclusive,
            UseChargeGroups = p.UseChargeGroups,
            ProductTypeId = p.ProductTypeId,
            ChargeGroupId = p.ChargeGroupId,
            Description = p.Description,
            Configuration = p.Configuration is null ? null : new SavingProductConfigDto
            {
                InterestBasedProduct = p.Configuration.InterestBasedProduct,
                InterestRate = p.Configuration.InterestRate,
                InterestDays = p.Configuration.InterestDays,
                InterestMethod = p.Configuration.InterestMethod.ToString(),
                ChargeWithholdingTaxOnSavingInterest = p.Configuration.ChargeWithholdingTaxOnSavingInterest,
                TurnOnOverdraftProtection = p.Configuration.TurnOnOverdraftProtection,
                OverdraftInterestRate = p.Configuration.OverdraftInterestRate,
                OverdraftPeriod = p.Configuration.OverdraftPeriod,
                ChargeInterestOnNegativeBalances = p.Configuration.ChargeInterestOnNegativeBalances,
                ConsiderDormantAfterDaysOfInactivity = p.Configuration.ConsiderDormantAfterDaysOfInactivity,
                HasChequeBook = p.Configuration.HasChequeBook,
                EnableSmsBanking = p.Configuration.EnableSmsBanking,
                EnableElectronicCardTransaction = p.Configuration.EnableElectronicCardTransaction,
                BookSavingsToGeneralLedger = p.Configuration.BookSavingsToGeneralLedger,
                AllowMultiCurrency = p.Configuration.AllowMultiCurrency,
                EnforceIndividualSaving = p.Configuration.EnforceIndividualSaving,
                MinimumBalanceIndividualAccounts = p.Configuration.MinimumBalanceIndividualAccounts,
                EnforceGroupSaving = p.Configuration.EnforceGroupSaving,
                MinimumBalanceGroupAccounts = p.Configuration.MinimumBalanceGroupAccounts,
                EnforceBusinessSaving = p.Configuration.EnforceBusinessSaving,
                MinimumBalanceBusinessAccounts = p.Configuration.MinimumBalanceBusinessAccounts,
                MinimumClientAge = p.Configuration.MinimumClientAge
            },
            PostingAccounts = p.PostingAccounts.Select(MapPostingAccount),
            Params = p.Params.Select(MapParam)
        };
        public static LoanProductSummaryDto MapLoanSummary(LoanProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            TargetGroup = p.TargetGroup.ToString(),
            IsActive = p.IsActive
        };

        public static LoanProductDetailDto MapLoanDetail(LoanProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            TargetGroup = p.TargetGroup.ToString(),
            IsActive = p.IsActive,
            UseClasses = p.UseClasses,
            VatInclusive = p.VatInclusive,
            UseChargeGroups = p.UseChargeGroups,
            ProductTypeId = p.ProductTypeId,
            ChargeGroupId = p.ChargeGroupId,
            Description = p.Description,
            PostingAccounts = p.PostingAccounts.Select(MapPostingAccount),
            Params = p.Params.Select(MapParam)
        };

        public static ShareProductSummaryDto MapShareSummary(ShareProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            IsActive = p.IsActive,
            NominalValue = p.Configuration?.NominalValue ?? 0,
            DividendMethod = p.Configuration?.DividendCalculationMethod.ToString() ?? string.Empty
        };

        public static ShareProductDetailDto MapShareDetail(ShareProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            IsActive = p.IsActive,
            NominalValue = p.Configuration?.NominalValue ?? 0,
            DividendMethod = p.Configuration?.DividendCalculationMethod.ToString() ?? string.Empty,
            VatInclusive = p.VatInclusive,
            UseChargeGroups = p.UseChargeGroups,
            ProductTypeId = p.ProductTypeId,
            ChargeGroupId = p.ChargeGroupId,
            Description = p.Description,
            Configuration = p.Configuration is null ? null : new ShareProductConfigDto
            {
                NominalValue = p.Configuration.NominalValue,
                MinimumShareCapital = p.Configuration.MinimumShareCapital,
                DividendCalculationMethod = p.Configuration.DividendCalculationMethod.ToString(),
                DividendCalculationPeriod = p.Configuration.DividendCalculationPeriod,
                DividendCalculationInterval = p.Configuration.DividendCalculationInterval.ToString(),
                DividendRate = p.Configuration.DividendRate,
                ChargeWithholdingTaxOnDividends = p.Configuration.ChargeWithholdingTaxOnDividends,
                AllowShareRedemption = p.Configuration.AllowShareRedemption,
                MinimumSharesAfterRedemption = p.Configuration.MinimumSharesAfterRedemption,
                RequireApprovalForRedemption = p.Configuration.RequireApprovalForRedemption
            },
            PostingAccounts = p.PostingAccounts.Select(MapPostingAccount),
            Params = p.Params.Select(MapParam)
        };
        public static TimedepositProductSummaryDto MapTimedepositSummary(TimedepositProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            IsActive = p.IsActive,
            Period = p.Period,
            PeriodType = p.PeriodType.ToString(),
            MinimumAmount = p.MinimumAmount,
            MaximumAmount = p.MaximumAmount,
            TierInterest = p.TierInterest
        };

        public static TimedepositProductDetailDto MapTimedepositDetail(TimedepositProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            IsActive = p.IsActive,
            Period = p.Period,
            PeriodType = p.PeriodType.ToString(),
            MinimumAmount = p.MinimumAmount,
            MaximumAmount = p.MaximumAmount,
            TierInterest = p.TierInterest,
            WithdrawMode = p.WithdrawMode.ToString(),
            CapitalizeInterest = p.CapitalizeInterest,
            ForfeitInterestForPrematureWithdraw = p.ForfeitInterestForPrematureWithdraw,
            PrematureWithdrawPenalty = p.PrematureWithdrawPenalty,
            TierMethod = p.TierMethod.ToString(),
            VatInclusive = p.VatInclusive,
            UseChargeGroups = p.UseChargeGroups,
            ProductTypeId = p.ProductTypeId,
            ChargeGroupId = p.ChargeGroupId,
            Description = p.Description,
            InterestRates = p.InterestRates.Select(r => new TimedepositRateDto
            {
                Id = r.Id,
                Period = r.Period,
                PeriodType = r.PeriodType.ToString(),
                InterestRate = r.InterestRate,
                MinimumAmount = r.MinimumAmount,
                IsActive = r.IsActive
            }),
            InterestTiers = p.InterestTiers.Select(t => new TimedepositTierDto
            {
                Id = t.Id,
                FromAmount = t.FromAmount,
                ToAmount = t.ToAmount,
                Rate = t.Rate,
                IsActive = t.IsActive
            }),
            PostingAccounts = p.PostingAccounts.Select(MapPostingAccount),
            Params = p.Params.Select(MapParam)
        };

        public static InsuranceProductSummaryDto MapInsuranceSummary(InsuranceProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            IsActive = p.IsActive,
            PolicyPeriod = p.Configuration?.PolicyPeriod ?? 0,
            MonthlyPremium = p.Configuration?.MonthlyPremium ?? 0
        };

        public static InsuranceProductDetailDto MapInsuranceDetail(InsuranceProduct p) => new()
        {
            Id = p.Id,
            Code = p.Code,
            ProductName = p.ProductName,
            ProductTypeName = p.ProductType?.Name ?? string.Empty,
            IsActive = p.IsActive,
            PolicyPeriod = p.Configuration?.PolicyPeriod ?? 0,
            MonthlyPremium = p.Configuration?.MonthlyPremium ?? 0,
            VatInclusive = p.VatInclusive,
            UseChargeGroups = p.UseChargeGroups,
            ProductTypeId = p.ProductTypeId,
            ChargeGroupId = p.ChargeGroupId,
            Description = p.Description,
            Configuration = p.Configuration is null ? null : new InsuranceProductConfigDto
            {
                PolicyPeriod = p.Configuration.PolicyPeriod,
                MinimumNumberInsured = p.Configuration.MinimumNumberInsured,
                MaximumNumberInsured = p.Configuration.MaximumNumberInsured,
                MinimumInsurableAge = p.Configuration.MinimumInsurableAge,
                MaximumInsurableAge = p.Configuration.MaximumInsurableAge,
                MonthlyPremium = p.Configuration.MonthlyPremium,
                PremiumPercentageCharged = p.Configuration.PremiumPercentageCharged,
                ChargeFixedAmount = p.Configuration.ChargeFixedAmount,
                FixedAmount = p.Configuration.FixedAmount,
                CanModifyPremium = p.Configuration.CanModifyPremium,
                MinimumCoverage = p.Configuration.MinimumCoverage,
                MaximumCoverage = p.Configuration.MaximumCoverage,
                Discount = p.Configuration.Discount,
                ClaimsPercentage = p.Configuration.ClaimsPercentage,
                AdministrationCostPercentage = p.Configuration.AdministrationCostPercentage,
                AdministrationFundPercentage = p.Configuration.AdministrationFundPercentage,
                ChargeWithholdingTaxOnCharges = p.Configuration.ChargeWithholdingTaxOnCharges,
                ChargeStampDutyOnPolicies = p.Configuration.ChargeStampDutyOnPolicies,
                RequireApprovalForClaims = p.Configuration.RequireApprovalForClaims,
                WaitingPeriodDays = p.Configuration.WaitingPeriodDays
            },
            PostingAccounts = p.PostingAccounts.Select(MapPostingAccount),
            Params = p.Params.Select(MapParam)
        };
    }
}
