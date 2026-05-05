using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Documents;
using Argent.Api.Domain.Entities.Accounting.Journals;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting.Vouchers {
    /// <summary>
    /// A voucher posting line — a payment, receipt, or journal voucher entry.
    /// Captures transactions outside day-to-day operations:
    /// prepayments, depreciation, FX revaluation, asset sales, recurring entries.
    ///
    /// Each voucher links to:
    ///   - A VoucherType (the posting configuration)
    ///   - A GeneralLedgerEntry (the GL side)
    ///   - Optionally a JournalEntry (the journal side for double-entry)
    ///   - A TransactionDocument (the physical document reference)
    ///
    /// RelatesTo: free-text reference to a customer/supplier/member code.
    /// Ref (CashLedgerFolio): classifies the cash transaction type.
    /// Payment: how payment was made (Cash, Cheque, Bank Transfer, Card).
    /// Authorized: name of person authorizing the payment.
    /// </summary>
    public class VoucherLine : BaseEntity {
        public string TransactionId { get; set; } = string.Empty;
        public DateTime PostedOn { get; set; }
        public string? Particulars { get; set; }
        public string? FolioCode { get; set; }
        public string? VoucherNumber { get; set; }

        /// <summary>
        /// Customer/supplier/member code this voucher relates to.
        /// </summary>
        public string? RelatesTo { get; set; }

        public CashLedgerFolio Ref { get; set; }
        public PaymentMethod Payment { get; set; }
        public PaymentStatus Clearance { get; set; }

        public decimal Debit { get; set; }
        public decimal Credit { get; set; }
        public decimal Discount { get; set; }

        /// <summary>
        /// Name of person authorizing this payment/receipt.
        /// </summary>
        public string? Authorized { get; set; }

        /// <summary>
        /// Code of the cashier who processed this voucher.
        /// </summary>
        public string? CashierCode { get; set; }
        public long VoucherTypeId { get; set; }
        public VoucherType VoucherType { get; set; } = null!;

        public long TransactionDocumentTypeId { get; set; }
        public TransactionDocumentType TransactionDocumentType { get; set; } = null!;

        public long TransactionDocumentId { get; set; }
        public TransactionDocument TransactionDocument { get; set; } = null!;

        public long? GeneralLedgerEntryId { get; set; }
        public GeneralLedgerEntry? GeneralLedgerEntry { get; set; }
    }
}
