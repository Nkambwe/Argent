using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// Replaces the AccessibleBranches comma-separated string.
    /// Explicitly grants a cashier access to work at a specific branch.
    /// </summary>
    public class CashierBranchAccess : BaseEntity {
        public long CashierId { get; set; }
        public Cashier Cashier { get; set; } = null!;
        public long BranchId { get; set; }
        public Branch Branch { get; set; } = null!;
    }
}
