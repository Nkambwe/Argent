using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Entities.Banking.Loans;

namespace Argent.Api.Domain.Entities.Products {
    public class LoanProduct : ProductBase {
        /// <summary>
        /// Get or set target group this loan is. <see cref="CustomerTarget"/> enumeration
        /// </summary>
        //public CustomerTarget TargetGroup { get; set; }
        public bool UseClasses { get; set; }
        public long? SectorId { get; set; }
        //public virtual BusinessSector Sector { get; set; }
        public long? FundId { get; set; }
        public virtual RevolvingFund Fund { get; set; } = null!;
        public long ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; } = null!;
        public long? ChargeGroupId { get; set; }
        public virtual ChargeGroup ChargeGroup { get; set; } = null!;
        public virtual ICollection<LoanChargeStage> ChargeStages { get; set; } = [];
        public virtual ICollection<LoanProductTaxGroup> TaxGroups { get; set; } = [];
        public virtual ICollection<TaxableItem> TaxableItems { get; set; } = [];
        public virtual ICollection<LoanProductChargeItem> LoanProductChargeItems { get; set; } = [];
        //public virtual ICollection<LoanProductParam> ProductParams { get; set; } = [];
        //public virtual ICollection<LoanProductApprovalStage> ApprovalStages { get; set; } = [];
        //public virtual ICollection<VariableRate> AdjustedRates { get; set; } = [];
        //public virtual ICollection<LoanFeePaymentLevel> FeesPaymentLevels { get; set; } = [];
        //public virtual ICollection<LoanAgingClass> AgingClasses { get; set; } = [];
        //public virtual ICollection<LoanAmountClass> LoanAmountClasses { get; set; } = [];
        //public virtual ICollection<LoanPenalty> Penalties { get; set; } = [];
        //public virtual ICollection<IndividualLoan> IndividualLoans { get; set; } = [];
        //public virtual ICollection<GroupLoan> GroupLoans { get; set; } = [];
        //public virtual ICollection<BusinessLoan> BusinessLoans { get; set; } = [];
    }
}
