using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// Account references provide analytical dimensions on general ledger postings.
    /// They allow transaction amounts to be attributed to specific business units,
    /// departments, cost centres, or any other analytical axis.
    ///
    /// Example: Reference "Department" with values "Operations", "Finance", "HR"
    /// allows GL postings to be reported by department.
    /// </summary>
    public class AccountReference : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public bool Active { get; set; } = true;

        /// <summary>
        /// System references cannot be deleted or renamed.
        /// </summary>
        public bool IsSystem { get; set; }

        public string? Notes { get; set; }

        public ICollection<LedgerAccountReference> LedgerAccounts { get; set; } = [];
        public ICollection<AccountReferenceValue> ReferenceValues { get; set; } = [];
    }
}
