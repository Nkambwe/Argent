using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class CreateTimedepositProductRequest {
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long ProductTypeId { get; set; }
        public long? ChargeGroupId { get; set; }
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
        public InterestWithdrawMode WithdrawMode { get; set; } = InterestWithdrawMode.AtMaturity;
        public bool CapitalizeInterest { get; set; }
        public bool ForfeitInterestForPrematureWithdraw { get; set; }
        public decimal PrematureWithdrawPenalty { get; set; }
        public int Period { get; set; }
        public IntervalType PeriodType { get; set; } = IntervalType.Months;
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public bool TierInterest { get; set; }
        public TierCalculationMethod TierMethod { get; set; } = TierCalculationMethod.None;
        public List<CreateTimedepositRateRequest> InterestRates { get; set; } = [];
        public List<CreateTimedepositTierRequest> InterestTiers { get; set; } = [];
        public List<UpsertPostingAccountRequest> PostingAccounts { get; set; } = [];
    }
}
