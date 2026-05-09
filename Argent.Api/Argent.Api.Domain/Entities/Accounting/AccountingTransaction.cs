using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// Abstract base for every financial transaction line posted in the system.
    ///
    /// Every module that posts to the general ledger inherits from this:
    ///   GeneralLedgerEntry (GL postings)
    ///   BankLedgerEntry    (bank reconciliation)
    ///   SavingsLedgerEntry (savings account postings)
    ///   LoanLedgerEntry    (loan repayments, disbursements)
    ///   ShareLedgerEntry   (share purchases, dividends)
    ///   ChargeLedgerEntry  (fee postings)
    ///   RegistrationLedgerEntry (membership fee postings)
    ///
    /// Double-entry is enforced at the application layer — every Debit must
    /// have a corresponding Credit in the same transaction batch.
    ///
    /// Reference1..6 carry analytical tags (account references, cost centres,
    /// branch codes, customer codes, product codes, etc.)
    /// </summary>
    public abstract class AccountingTransaction : BaseEntity {
        /// <summary>
        /// Batch identifier — all lines with the same code form one double-entry set.
        /// </summary>
        public string TransactionCode { get; set; } = string.Empty;
        public DateTime PostedOn { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Narrative description of this line.
        /// </summary>
        public string Particulars { get; set; } = string.Empty;

        /// <summary>
        /// Folio code (description template code) for this posting.
        /// </summary>
        public string? FolioCode { get; set; }

        /// <summary>
        /// GL account number this line posts to.
        /// </summary>
        public string LedgerNumber { get; set; } = string.Empty;

        /// <summary>
        /// Document/voucher series this posting belongs to.
        /// </summary>
        public string PostingSeries { get; set; } = string.Empty;

        /// <summary>
        /// Voucher reference number
        /// </summary>
        public string VoucherNumber { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }

        /// <summary>
        /// Currency code of this line (may differ from base currency).
        /// </summary>
        public string? CurrencyCode { get; set; }

        /// <summary>
        /// Amount in base currency after exchange rate conversion.
        /// </summary>
        public decimal ExchangeAmount { get; set; }

        #region Cross-reference codes
        /// <summary>
        /// general GL reference
        /// </summary>
        public string? GeneralReference { get; set; }   
        /// <summary>
        /// business unit reference
        /// </summary>
        public string? BusinessReference { get; set; }
        /// <summary>
        /// charge or fee reference
        /// </summary>
        public string? ChargeReference { get; set; }     
        public string? Reference1 { get; set; }
        public string? Reference2 { get; set; }
        public string? Reference3 { get; set; }
        public string? Reference4 { get; set; }
        public string? Reference5 { get; set; }
        public string? Reference6 { get; set; }
        #endregion

        #region Tax
        public string? TaxCode { get; set; }
        public decimal TaxCharge1 { get; set; }
        public decimal TaxCharge2 { get; set; }
        #endregion

        public bool Closed { get; set; }
        public DateTime? ClosedOn { get; set; }
        public string? Comment { get; set; }
        public string? Cashier { get; set; }
        /// <summary>
        /// The accounting period (MonthlyClosure) this transaction belongs to.
        /// Prevents posting to closed periods.
        /// </summary>
        public long? MonthlyClosureId { get; set; }
        public MonthlyClosure? MonthlyClosure { get; set; }  
    }
}
