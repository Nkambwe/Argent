
namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A group or section heading in the chart of accounts hierarchy.
    /// Headers do not post transactions — they group LedgerAccounts.
    /// e.g. "1000 — Current Assets", "2000 — Current Liabilities"
    /// </summary>
    public class LedgerAccountHeader : AccountBase {
        /// <summary>
        /// LedgerNumber of the parent header 
        /// </summary>
        public string? ParentHeader { get; set; }
        public ICollection<LedgerAccount> LedgerAccounts { get; set; } = [];
        public ICollection<LedgerAccountTotal> TotalLabels { get; set; } = [];
    }
   
}


