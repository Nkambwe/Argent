using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// A physical branch of a Bank.
    /// BankBranch is distinct from the organization's own Branch entity —
    /// this is the external bank's branch where the organization holds accounts.
    /// </summary>
    public class BankBranch : BaseEntity {
        public string BranchCode { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string? BranchAddress { get; set; }
        public string? BranchContact { get; set; }
        public string? ContactDesignation { get; set; }
        public string? ContactEmail { get; set; }
        public string? PrimaryLine { get; set; }
        public string? SecondaryLine { get; set; }
        public string? BranchFax { get; set; }

        public Guid BankId { get; set; }
        public Bank Bank { get; set; } = null!;

        public ICollection<BankAccount> Accounts { get; set; } = [];
    }
}
