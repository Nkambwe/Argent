using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Entities.Accounting.Vouchers;

namespace Argent.Api.Domain.Entities.Accounting.Journals {
    /// <summary>
    /// A journal voucher entry for non-routine accounting adjustments:
    /// prepayments, accruals, depreciation, asset disposals, FX revaluation,
    /// opening balances, and year-end adjustments.
    ///
    /// Inherits AccountingTransaction for all shared posting fields.
    ///
    /// Lifecycle flags:
    ///   UnPosted  = draft / not yet committed to the GL
    ///   Approved  = authorized by a second user
    ///   Voided    = cancelled before posting (no GL effect)
    ///   Reversed  = posted and then reversed (creates counter-entry)
    ///
    /// SalesTax: tax computation attached to this journal entry.
    /// VoucherLines: cash/bank vouchers that originated from this journal.
    /// </summary>
    public class JournalEntry : AccountingTransaction {
        public long JournalTypeId { get; set; }
        public JournalType JournalType { get; set; } = null!;
        public bool UnPosted { get; set; } = true;   
        public bool Approved { get; set; }
        public string? ApprovedBy { get; set; }
        public bool Voided { get; set; }
        public bool Reversed { get; set; }
        public string TransactionId { get; set; } = string.Empty;

        public long? GeneralLedgerEntryId { get; set; }
        public GeneralLedgerEntry? GeneralLedgerEntry { get; set; }
    }
}
