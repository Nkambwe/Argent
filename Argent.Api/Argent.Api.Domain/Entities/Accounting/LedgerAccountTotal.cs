namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// Defines a subtotal or summary line across a range of ledger accounts.
    /// e.g. "Total Current Assets: 101001000 → 101009000"
    /// Used in financial statement presentation.
    /// </summary>
    public class LedgerAccountTotal : AccountBase {
        public long LedgerAccountHeaderId { get; set; }
        public LedgerAccountHeader LedgerAccountHeader { get; set; } = null!;

        /// <summary>
        /// Ledger number range that is summed for this total.
        /// Format: "101001000...101009000"
        /// </summary>
        public string TotalRange { get; set; } = string.Empty;
    }

}
