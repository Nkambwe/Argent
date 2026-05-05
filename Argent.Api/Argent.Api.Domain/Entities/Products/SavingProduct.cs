using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Accounting.Taxes;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// Savings product record
    /// </summary>
    public class SavingProduct : ProductBase {
        /// <summary>
        /// Get/Set whether product has a limit on the number of withdraws in a month
        /// </summary>
        public bool LimitWithdraw { get; set; }
        /// <summary>
        /// Get/Set the maximum number of withdraws allowed in a month
        /// </summary>
        public int MaximumWithdraws { get; set; }
        /// <summary>
        /// Get/Set penalty charged due to exceeding maximum withdraws
        /// </summary>
        public decimal WithdrawPenalty { get; set; }
        /// <summary>
        /// Get/Set whether savings product adds a charge on withdraws
        /// </summary>
        public bool ChargeWithdraws { get; set; }
        /// <summary>
        /// Get/Set whether savings product allows overdraft loans on savings accounts
        /// </summary>
        public bool AllowOverdraft { get; set; }
        /// <summary>
        /// Get/Set interest charged on overdraft
        /// </summary>
        public decimal OverdraftInterest { get; set; }
        /// <summary>
        /// Get/Set required minimum balance on savings account for this product
        /// </summary>
        public decimal MinimumBalance { get; set; }
        /// <summary>
        /// Get/Set whether savings product offers interest on client savings
        /// </summary>
        public bool OfferInterest { get; set; }
        /// <summary>
        /// Get/Set annual interest rate offered on a loan
        /// </summary>
        public decimal InterestRate { get; set; }
        /// <summary>
        /// Get/Set the minimum interest amount that can be offered
        /// </summary>
        public decimal MinimumInterestOffered { get; set; }
        public long ProductTypeId { get; set; }
        public virtual ProductType? ProductType { get; set; }
        public long? ChargeGroupId { get; set; }
        public virtual ChargeGroup? ChargeGroup { get; set; }
        public virtual ICollection<SavingProductTaxGroup> TaxGroups { get; set; } = [];
        public virtual ICollection<ChargeStage> ChargeStages { get; set; } = [];
        public virtual ICollection<TaxableItem> TaxableItems { get; set; } = [];
        public virtual ICollection<ChargeItem> ChargedItems { get; set; } = [];
        //public virtual ICollection<SavingProductParam> ProductParams { get; set; } = [];
        //public virtual ICollection<WithdrawClass> WithdrawClasses { get; set; } = [];
        //public virtual ICollection<SavingAccount> SavingAccounts { get; set; } = [];
    }
}
