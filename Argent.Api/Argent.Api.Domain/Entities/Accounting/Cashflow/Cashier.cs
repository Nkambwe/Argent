using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Access;
using Argent.Api.Domain.Entities.Accounting.Journals;
using Argent.Api.Domain.Entities.Accounting.Vouchers;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// A system user acting as cashier — authorized to post to cash accounts,
    /// process vouchers, and create journal entries.
    ///
    /// LowerLimit / UpperLimit control the transaction amount range this cashier can handle.
    /// DefaultAccount: the cash account code this cashier defaults to.
    ///
    /// Branch access is modelled as CashierBranchAccess (replaces the AccessibleBranches
    /// comma-separated string from the source — same pattern we used for UserBranchAccess).
    /// </summary>
    public class Cashier : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The system user record this cashier profile belongs to.
        /// </summary>
        public long? UserId { get; set; }
        public AppUser User { get; set; } = null!;

        /// <summary>Primary branch this cashier operates from.</summary>
        public string? CurrentBranch { get; set; }

        /// <summary>Default cash account code for this cashier.</summary>
        public string? DefaultAccount { get; set; }

        public decimal LowerLimit { get; set; }
        public decimal UpperLimit { get; set; }

        public ICollection<CashierAccount> CashierAccounts { get; set; } = [];
        public ICollection<CashierBranchAccess> BranchAccess { get; set; } = [];
        public ICollection<CashierJournalType> JournalTypes { get; set; } = [];
        public ICollection<CashierVoucherType> VoucherTypes { get; set; } = [];
    }
}
