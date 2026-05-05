using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// Abstract base shared by LedgerAccount, LedgerAccountHeader, and LedgerAccountTotal.
    /// Carries the classification, grouping, and indexing fields used across
    /// the chart of accounts hierarchy.
    /// </summary>
    public abstract class AccountBase : BaseEntity {
        public string LedgerNumber { get; set; } = string.Empty;
        public string LedgerName { get; set; } = string.Empty;
        public AccountClassification AccountClassification { get; set; }
        public AccountCategory AccountCategory { get; set; }
        public AccountNature AccountNature { get; set; }

        /// <summary>
        /// Sort order within the account group.
        /// </summary>
        public long GroupIndex { get; set; }

        /// <summary>
        /// Sort order within the ledger list.
        /// </summary>
        public long LedgerIndex { get; set; }
    }
}


