using Argent.Api.Infrastructure.Core.Modules.Products.OverrideObjects;

namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class CreateSavingProductRequest {
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public long ProductTypeId { get; set; }
        public long? ChargeGroupId { get; set; }
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
        public bool LimitWithdraw { get; set; }
        public int MaximumWithdraws { get; set; }
        public decimal WithdrawPenalty { get; set; }
        public bool ChargeWithdraws { get; set; }
        public bool AllowOverdraft { get; set; }
        public decimal OverdraftInterest { get; set; }
        public decimal MinimumBalance { get; set; }
        public bool OfferInterest { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MinimumInterestOffered { get; set; }

        /// <summary>
        /// Optional initial configuration overrides.
        /// If not provided, defaults are seeded from the module configuration.
        /// </summary>
        public SavingProductConfigOverrides? ConfigOverrides { get; set; }

        /// <summary>
        /// Initial posting account mappings for this product.
        /// </summary>
        public List<UpsertPostingAccountRequest> PostingAccounts { get; set; } = [];
    }
}
