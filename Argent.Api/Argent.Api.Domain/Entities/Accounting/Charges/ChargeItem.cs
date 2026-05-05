using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    /// <summary>
    /// A specific line-item charge configuration for a product.
    /// A ChargeItem belongs to exactly one product (tracked via ProductReference)
    /// and contains the rate or amount, the GL ledger code it posts to,
    /// and an optional tax configuration.
    ///
    /// Multiple ChargeItems can be linked to a Charge via ChargeItemCharge (M:M).
    /// </summary>
    public class ChargeItem : BaseEntity {
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Numeric code indicating what event triggers this charge.
        /// </summary>
        public int ChargeOn { get; set; }

        public string Description { get; set; } = string.Empty;
        public decimal FixedAmount { get; set; }
        public bool IsRated { get; set; }
        public decimal Percentage { get; set; }

        /// <summary>
        /// GL ledger account number this charge posts to.
        /// </summary>
        public string? LedgerCode { get; set; }
        public long? TaxId { get; set; }
        //public Tax Tax { get; set; }    // filled when Tax module is built
        public long? ProductId { get; set; }
        public ProductType? ProductType { get; set; }

        public ICollection<ChargeItemCharge> Charges { get; set; } = [];
        public ICollection<ChargeStage> ChargeStages { get; set; } = [];
        public ICollection<ChargeLedgerEntry> ChargeLedgerEntries { get; set; } = [];
        public ICollection<RegistrationLedgerEntry> RegistrationLedgerEntries { get; set; } = [];
    }

}
