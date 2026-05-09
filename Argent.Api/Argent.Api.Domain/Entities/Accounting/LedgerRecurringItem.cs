using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A scheduled automatic GL posting — e.g. monthly rent, salaries, depreciation.
    ///
    /// Posts from Ledger (debit account) to Against (credit account) on the specified
    /// day of each month (Every). Auto = true means the system posts without user action.
    ///
    /// Linked to a Branch for branch-level recurring entries.
    /// </summary>
    public class LedgerRecurringItem : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public DateOnly StartsOn { get; set; }
        public DateOnly? EndsOn { get; set; }

        public decimal Amount { get; set; }

        /// <summary>
        /// Day of the month this posting triggers (1–31).
        /// </summary>
        public int PostDay { get; set; }

        public PostingType PostingType { get; set; }

        /// <summary>
        /// True: system auto-posts on the trigger date. False: requires manual approval.
        /// </summary>
        public bool AutoPost { get; set; }

        /// <summary>
        /// GL ledger number to debit.
        /// </summary>
        public string DebitLedger { get; set; } = string.Empty;

        /// <summary>
        /// GL ledger number to credit.
        /// </summary>
        public string CreditLedger { get; set; } = string.Empty;

        /// <summary>
        /// Optional branch restriction — null = applies to all branches.
        /// </summary>
        public long? BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
    }
}
