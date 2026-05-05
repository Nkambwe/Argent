using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A card transaction posted against a GeneralLedgerEntry.
    /// Links the card instrument to the GL posting for reporting.
    /// </summary>
    public class CardLedgerEntry : BaseEntity {
        public long CardId { get; set; }
        public Card Card { get; set; } = null!;
        /// <summary>
        /// card network reference
        /// </summary>
        public string? ExternalTransactionId { get; set; }  
        public DateTime TransactionDate { get; set; }
        public string? Particulars { get; set; }
        public string? FolioCode { get; set; }
        public decimal Amount { get; set; }
        public long GeneralLedgerEntryId { get; set; }
        public GeneralLedgerEntry GeneralLedgerEntry { get; set; } = null!;
    }
}
