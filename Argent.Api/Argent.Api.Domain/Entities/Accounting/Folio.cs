using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A transaction description template attached to a ledger account.
    /// Controls how posting narrations are generated for journal entries.
    /// </summary>
    public class Folio : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Particulars { get; set; } = string.Empty;
        public string? Notes { get; set; }

        public long FolioTypeId { get; set; }
        public FolioType FolioType { get; set; } = null!;

        public ICollection<LedgerAccount> LedgerAccounts { get; set; } = [];
    }
}
