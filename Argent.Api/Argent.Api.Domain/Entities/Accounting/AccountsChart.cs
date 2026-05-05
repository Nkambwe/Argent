using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A chart of accounts is a defined set of ledger accounts for a branch or the whole organization.
    /// Multiple charts can exist (e.g. one per branch, one standard template).
    /// </summary>
    public class AccountsChart : BaseEntity {
        public string ChartName { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ChartType ChartType { get; set; }
        public ICollection<Branch> Branches { get; set; } = [];
        public ICollection<LedgerAccount> LedgerAccounts { get; set; } = [];
    }
}


