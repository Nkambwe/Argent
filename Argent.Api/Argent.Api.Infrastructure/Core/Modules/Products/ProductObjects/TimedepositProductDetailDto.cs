namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class TimedepositProductDetailDto : TimedepositProductSummaryDto {
        public string WithdrawMode { get; set; } = string.Empty;
        public bool CapitalizeInterest { get; set; }
        public bool ForfeitInterestForPrematureWithdraw { get; set; }
        public decimal PrematureWithdrawPenalty { get; set; }
        public string TierMethod { get; set; } = string.Empty;
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
        public long ProductTypeId { get; set; }
        public long? ChargeGroupId { get; set; }
        public string? Description { get; set; }
        public IEnumerable<TimedepositRateDto> InterestRates { get; set; } = [];
        public IEnumerable<TimedepositTierDto> InterestTiers { get; set; } = [];
        public IEnumerable<PostingAccountDto> PostingAccounts { get; set; } = [];
        public IEnumerable<ProductParamDto> Params { get; set; } = [];
    }

}
