using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Argent.Api.Domain.Entities.Accounting.Documents;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A bank account statement line used for bank reconciliation.
    /// NOT a subclass of AccountingTransaction — bank entries are sourced from
    /// bank statements and matched against GL entries, not created by the system.
    /// </summary>
    /// <remarks>
    /// TransactionType: Deposit or Withdrawal
    /// TransactionNature: Credit or Debit side
    /// Clearance (PaymentStatus): whether this entry has been reconciled/cleared 
    /// </remarks>
    public class BankLedgerEntry : BaseEntity {
        public string TransactionCode { get; set; } = string.Empty;
        public DateTime PostedOn { get; set; }
        public string FolioCode { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VoucherNumber { get; set; } = string.Empty;
        public string LedgerCode { get; set; } = string.Empty;
        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Balance { get; set; }

        /// <summary>
        /// Currency code of this bank entry (may differ from base currency).
        /// </summary>
        public string CurrencyCode { get; set; } = string.Empty;
        public BankTransactionType TransactionType { get; set; }
        public BankTransactionNature Nature { get; set; }
        public PaymentStatus Clearance { get; set; }
        /// <summary>
        /// True when this entry has been matched in bank reconciliation.
        /// </summary>
        public bool Reconciled { get; set; }
        public long BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; } = null!;
        public long? TransactionDocumentId { get; set; }
        public TransactionDocument TransactionDocument { get; set; } = null!;
        public ICollection<ChequeLedgerEntry> ChequeEntries { get; set; } = [];
    }

}
