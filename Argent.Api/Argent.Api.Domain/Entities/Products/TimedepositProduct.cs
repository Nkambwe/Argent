using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// Timedeposit product record
    /// </summary>
    public partial class TimedepositProduct : ProductBase {
        public InterestWithdrawMode WithdrawMode { get; set; }
        public bool CapitalizeInterest { get; set; }
        /// <summary>
        /// Get/Set if all interest is forfeited on premature withdraws
        /// </summary>
        public bool ForfeitInterestForPrematureWithdraw { get; set; }
        /// <summary>
        /// Get/Set percentage of penalty forfeited if not all interest is forfeited at premature withdraw
        /// </summary>
        public decimal PrematureWithdrawsPenalty { get; set; }
        /// <summary>
        /// Get/Set number of time period for the account
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// Get/Set type of period for this account. Eg. Days, Weeks, Months, or Years
        /// </summary>
        public IntervalType PeriodType { get; set; }
        /// <summary>
        /// Get/Set minimum acceptable amount to be fixed
        /// </summary>
        public decimal MinimumAmount { get; set; }
        /// <summary>
        /// Get/Set Maximum acceptable amount to be fixed
        /// </summary>
        public decimal MaximumAmount { get; set; }
        /// <summary>
        /// Get/Set whether interest
        /// </summary>
        public bool TierInterest { get; set; }
        /// <summary>
        /// Get/Set interest calculation methods
        /// </summary>
        //public TierCalculationMethod TierMethod { get; set; }
        public long ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public long? ChargeGroupId { get; set; }
        public virtual ChargeGroup? ChargeGroup { get; set; }
        //public virtual ICollection<TimedepositAccount> TimedepositAccounts { get; set; } = [];
        //public virtual ICollection<TimedepositRate> InterestRates { get; set; } = [];
        //public virtual ICollection<InterestTier> InterestTiers { get; set; } = [];
        public virtual ICollection<ChargeStage> ChargeStages { get; set; } = [];
        public virtual ICollection<TaxableItem> TaxableItems { get; set; } = [];
        public virtual ICollection<ChargeItem> ChargedItems { get; set; } = [];
        //public virtual ICollection<TimedepositProductParam> ProductParams { get; set; } = [];
        //public virtual ICollection<TimedepositProductTaxGroup> TaxGroups { get; set; }
    }
}
