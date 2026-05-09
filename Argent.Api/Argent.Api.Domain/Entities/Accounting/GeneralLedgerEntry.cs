using Argent.Api.Domain.Entities.Accounting.Journals;
using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Entities.Accounting.Vouchers;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A single line in the general ledger — the atomic unit of double-entry bookkeeping.
    /// Every financial event in every module (savings deposit, loan repayment, share purchase,
    /// charge collection, etc.) eventually becomes one or more GeneralLedgerEntry rows.
    ///
    /// Inherits AccountingTransaction for all shared posting fields:
    /// TransactionCode, PostedOn, Debit, Credit, References 1-6, Tax, Cashier, etc.
    ///
    /// The LedgerAccountId FK is the critical link to the chart of accounts.
    /// MonthlyClosureId prevents posting to closed accounting periods.
    /// </summary>
    public class GeneralLedgerEntry : AccountingTransaction {
        /// <summary>
        /// The GL account this line posts to.
        /// Stored as both FK (for integrity) and LedgerNumber string (for fast reporting).
        /// </summary>
        public long LedgerAccountId { get; set; }
        public LedgerAccount LedgerAccount { get; set; } = null!;

        /// <summary
        /// >Tax group applied to this posting line, if any.
        /// </summary>
        public long? TaxGroupId { get; set; }
        public TaxGroup TaxGroup { get; set; } = null!;

        public ICollection<CardLedgerEntry> CardEntries { get; set; } = [];
        public ICollection<VoucherEntry> VoucherLines { get; set; } = [];
        public ICollection<JournalEntry> JournalEntries { get; set; } = [];
    }
}
