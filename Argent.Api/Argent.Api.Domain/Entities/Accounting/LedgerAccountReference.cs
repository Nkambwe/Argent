using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// Junction: which account references apply to which ledger accounts.
    /// </summary>
    public class LedgerAccountReference : BaseEntity {
        public long LedgerAccountId { get; set; }
        public LedgerAccount LedgerAccount { get; set; } = null!;

        public long ReferenceId { get; set; }
        public AccountReference Reference { get; set; } = null!;
    }
}


