using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {

    /// <summary>
    /// An individual cheque leaf within a ChequeBook.
    /// Tracks issuance, clearance, and reversal lifecycle.
    /// </summary>
    public class Cheque : BaseEntity {
        public long ChequeBookId { get; set; }
        public ChequeBook ChequeBook { get; set; } = null!;
        public string Number { get; set; } = string.Empty;
        public string IssuerAccount { get; set; } = string.Empty;
        public string Recipient { get; set; } = string.Empty;
        public string RecipientAccount { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string AmountInWords { get; set; } = string.Empty;
        public ChequeStatus Status { get; set; } = ChequeStatus.New;
        /// <summary>
        /// True when the payment made by this cheque has been reversed.
        /// </summary>
        public bool Reversed { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
