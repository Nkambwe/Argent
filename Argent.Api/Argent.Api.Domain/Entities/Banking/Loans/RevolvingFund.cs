using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Currencies;

namespace Argent.Api.Domain.Entities.Banking.Loans {
    /// <summary>
    /// Class holds details of loan revolving fund
    /// </summary>
    public class RevolvingFund : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public DateTime? Started { get; set; }
        public DateTime? Ended { get; set; }

        /// <summary>
        /// Get/Set whether fund is based on savings lending
        /// </summary>
        public bool SavingsBased { get; set; }
        public decimal LoanablePercentage { get; set; }

        public long CurrencyId { get; set; }
        public virtual Currency? Currency { get; set; }

        public long? DonorId { get; set; }
        //public Donor Donor { get; set; }
        //public virtual ICollection<BranchRevolvingFund> Branches { get; set; } = [];
        //public virtual ICollection<LoanProduct> LoanProducts { get; set; } = [];
        //public virtual ICollection<IndividualLoan> IndividualLoans { get; set; } = [];
        //public virtual ICollection<GroupLoan> GroupLoans { get; set; } = [];
        //public virtual ICollection<BusinessLoan> BusinessLoans { get; set; } = [];

        public override string ToString() => $"{Code.Trim()}-{Name.Trim()}";
        public override int GetHashCode() => ToString().GetHashCode() ^ 3;
    }
}
