using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Postings {
    /// <summary>
    /// Posting groups define the automatic GL account mapping for different
    /// categories of transactions. When a journal or voucher is posted,
    /// the system looks up the posting group to determine which GL accounts
    /// to debit and credit automatically.
    /// </summary>
    /// <remarks>
    /// Three posting group types work together on each transaction:
    ///   GeneralPostingGroup  — the "what" (Sales, Purchases, Payroll, etc.)
    ///   BranchPostingGroup   — the "where" (which branch cost centre)
    ///   BusinessPostingGroup — the "who"  (Customer, Supplier, Staff)
    ///
    /// The intersection of these three groups resolves to the specific
    /// GL accounts used for each transaction line.
    ///
    /// This is a well-established pattern from ERP systems (NAV/BC style).
    /// The full posting group setup sub-module will be built in the
    /// Accounting configuration phase — these are skeleton entities
    /// that satisfy the FKs on JournalType and VoucherType.
    /// </remarks>
    public class GeneralPostingGroup : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }
        public string? SalesAccountNumber { get; set; }
        public string? PurchasesAccountNumber { get; set; }
        public string? DiscountAccountNumber { get; set; }
        public string? CostOfGoodsAccountNumber { get; set; }
    }
}
