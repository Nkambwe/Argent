using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Entities.Banking.Loans;
using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// A time deposit product defines the rules for fixed-term deposits.
    /// Members place a fixed amount for a fixed period and earn interest
    /// at a pre-agreed rate.
    ///
    /// TierInterest enables tiered interest rates based on deposit amount.
    /// TierMethod controls how tiers are applied (PerBand vs WholeAmount).
    /// InterestRates holds the rate schedule; InterestTiers holds the amount bands.
    /// </summary>
    public partial class TimedepositProduct : ProductBase {
        public InterestWithdrawMode WithdrawMode { get; set; } = InterestWithdrawMode.AtMaturity;
        public bool CapitalizeInterest { get; set; }
        /// <summary>
        /// Get/Set if all interest is forfeited on premature withdraws
        /// </summary>
        public bool ForfeitInterestForPrematureWithdraw { get; set; }
        /// <summary>
        /// Get/Set percentage of penalty forfeited if not all interest is forfeited at premature withdraw
        /// </summary>
        public decimal PrematureWithdrawPenalty { get; set; }
        /// <summary>
        /// Get/Set number of time period for the account
        /// </summary>
        public int Period { get; set; }
        /// <summary>
        /// Get/Set type of period for this account. Eg. Days, Weeks, Months, or Years
        /// </summary>
        public IntervalType PeriodType { get; set; } = IntervalType.Months;
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
        public TierCalculationMethod TierMethod { get; set; } = TierCalculationMethod.None;
        public long ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public long? ChargeGroupId { get; set; }
        public virtual ChargeGroup? ChargeGroup { get; set; }
        public TimedepositProductConfiguration? Configuration { get; set; }
        public override ProductModuleType Module => ProductModuleType.TimeDeposit;
        public virtual ICollection<LoanChargeStage> ChargeStages { get; set; } = [];
        public virtual ICollection<TaxableItem> TaxableItems { get; set; } = [];
        public virtual ICollection<TimedepositProductChargeItem> TimedepositProductChargeItems { get; set; } = [];
        /// <summary>
        /// Rate schedule — used when TierInterest is false.
        /// Multiple rows allow different rates for different term lengths.
        /// </summary>
        public ICollection<TimedepositRate> InterestRates { get; set; } = [];
        public ICollection<ProductParam> Params { get; set; } = [];
        /// <summary>
        /// Amount-based interest tiers — used when TierInterest is true.
        /// e.g. 0–5M = 8%, 5M–20M = 10%, 20M+ = 12%.
        /// </summary>
        public ICollection<TimedepositInterestTier> InterestTiers { get; set; } = [];
        public virtual ICollection<TimedepositProductTaxGroup> TaxGroups { get; set; } = [];
        public ICollection<ProductPostingAccount> PostingAccounts { get; set; } = [];
        //
        //public virtual ICollection<TimedepositAccount> TimedepositAccounts { get; set; } = [];
        //public virtual ICollection<InterestTier> InterestTiers { get; set; } = [];
    }
}
