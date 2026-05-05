using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Documents {
    /// <summary>
    /// Classifies the kind of document attached to a transaction.
    /// </summary>
    /// <remarks>
    /// Examples: Receipt, Payment Voucher, Journal Voucher, Bank Statement, Invoice, Credit Note, Disbursement Slip.
    /// Each type can have multiple SeriesNumber sequences for auto-numbering. 
    /// </remarks>
    public class TransactionDocumentType : BaseEntity {
        /// <summary>
        /// Type code e.g. "RCP", "PV", "JV"
        /// </summary>
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Type name e.g. "Receipt", "Payment Voucher"
        /// </summary>
        public string TypeName { get; set; } = string.Empty;
        /// <summary>
        /// Check if is system generated. system types cannot be deleted
        /// </summary>
        public bool IsSystem { get; set; }                     
        public string? Notes { get; set; }

        public ICollection<SeriesNumber> SeriesNumbers { get; set; } = [];
        public ICollection<TransactionDocument> Documents { get; set; } = [];
    }
}
