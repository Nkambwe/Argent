using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// Records the lifecycle movement of a cheque through its status changes.
    /// Each status change (Issued → Cleared, Issued → Bounced, etc.) creates
    /// a ChequeLedgerEntry that is linked to the originating BankLedgerEntry.
    /// </summary>
    public class ChequeLedgerEntry : BaseEntity {
        public long ChequeId { get; set; }
        public Cheque Cheque { get; set; } = null!;
        public DateTime TransactionDate { get; set; }
        public string? FolioCode { get; set; }
        public string? Particulars { get; set; }
        public ChequeStatus Status { get; set; }
        public decimal Amount { get; set; }

        public ICollection<BankLedgerEntry> BankEntries { get; set; } = [];
    }
}
