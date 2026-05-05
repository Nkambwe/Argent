using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Vouchers;

namespace Argent.Api.Domain.Entities.Accounting.Documents {
    /// <summary>
    /// A reference document attached to one or more transactions.
    /// Carries the document number (e.g. receipt number, voucher number)
    /// and links it to the document type for classification.
    ///
    /// One TransactionDocument can be referenced by multiple voucher or ledger lines,
    /// allowing a single receipt to cover multiple posting lines.
    /// </summary>
    public class TransactionDocument : BaseEntity {
        /// <summary>
        /// The transaction code this document relates to.
        /// Used to find all posting lines for this document across the ledger.
        /// </summary>
        public string TransactionCode { get; set; } = string.Empty;

        public string DocumentNumber { get; set; } = string.Empty;
        public string DocumentName { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public Guid DocumentTypeId { get; set; }
        public TransactionDocumentType DocumentType { get; set; } = null!;

        public ICollection<VoucherLine> VoucherLines { get; set; } = [];
        public ICollection<BankLedgerEntry> BankLines { get; set; } = [];
    }
}
