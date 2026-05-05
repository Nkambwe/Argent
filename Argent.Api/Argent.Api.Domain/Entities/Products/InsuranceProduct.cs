using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Accounting.Taxes;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// Insurance product
    /// </summary>
    public class InsuranceProduct : ProductBase {
        public long CoverageId { get; set; }
        public int Period { get; set; }
        public bool AllowPremiumModification { get; set; }
        /// <summary>
        /// Get/Set whether to charge premium per month
        /// </summary>
        public bool ChargeMonthlyPremium { get; set; }
        /// <summary>
        /// Get/Set percentage set as administrative costs
        /// </summary>
        public decimal PercentageAdministrativeAmount { get; set; }
        /// <summary>
        /// Get/Set administrative cost ledger account
        /// </summary>
        public string AdministrativeCostLedgerAccount { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set percentage set as claim amount
        /// </summary>
        public decimal PercentageClaimAmount { get; set; }
        /// <summary>
        /// Get/Set claim amount ledger account
        /// </summary>
        public string ClaimLedgerAccount { get; set; } = string.Empty;
        public int MinimumInsuredPersons { get; set; }
        public int MaximumInsuredPersons { get; set; }
        public int MinimumInsuredAge { get; set; }
        public int MaximumInsuredAge { get; set; }
        public decimal Fees { get; set; }
        public string FeesLedgerAccount { get; set; } = string.Empty;
        public long ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public long? ChargeGroupId { get; set; }
        public virtual ChargeGroup? ChargeGroup { get; set; }
        //public virtual Coverage Coverage { get; set; }
        //public virtual ICollection<Policy> Policies { get; set; }
        //public virtual ICollection<InsuranceProductProvider> Providers { get; set; }
        public virtual ICollection<ChargeStage> ChargeStages { get; set; } = [];
        //public virtual ICollection<InsuranceProductTaxGroup> TaxGroups { get; set; }
        public virtual ICollection<TaxableItem> TaxableItems { get; set; } = [];
        public virtual ICollection<ChargeItem> ChargedItems { get; set; } = [];
        //public virtual ICollection<InsurancetProductParam> ProductParams { get; set; } = [];
    }
}
