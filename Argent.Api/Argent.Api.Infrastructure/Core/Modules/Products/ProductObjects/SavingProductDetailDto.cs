namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class SavingProductDetailDto : SavingProductSummaryDto {
        public bool LimitWithdraw { get; set; }
        public int MaximumWithdraws { get; set; }
        public decimal WithdrawPenalty { get; set; }
        public bool ChargeWithdraws { get; set; }
        public decimal OverdraftInterest { get; set; }
        public decimal MinimumInterestOffered { get; set; }
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
        public long ProductTypeId { get; set; }
        public long? ChargeGroupId { get; set; }
        public string? Description { get; set; }
        public SavingProductConfigDto? Configuration { get; set; }
        public IEnumerable<PostingAccountDto> PostingAccounts { get; set; } = [];
        public IEnumerable<ProductParamDto> Params { get; set; } = [];
    }

}
