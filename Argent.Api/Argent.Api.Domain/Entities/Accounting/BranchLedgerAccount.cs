using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting {
    public class BranchLedgerAccount : BaseEntity {
        /// <summary>
        /// Get Or Set default ledger number in the company  default chart of accounts
        /// </summary>
        public string LedgerAccountNumber { get; set; } = string.Empty;
        /// <summary>
        /// Get Or Set new branch ledger number for this branch
        /// </summary>
        public string BranchAccountNumber { get; set; } = string.Empty;
        public bool Suspend { get; set; }
        public long LedgerAccountId { get; set; }
        public virtual LedgerAccount LedgerAccount { get; set; } = null!;
        public long BranchId { get; set; }
        public virtual Branch Branch { get; set; } = null!;

    }
}


