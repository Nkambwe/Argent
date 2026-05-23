using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// Maps a product to a general ledger account for a specific posting purpose
    /// and customer segment combination.
    ///
    /// This replaces the 40+ string ledger fields in the original configuration classes
    /// with a clean, queryable, extensible table.
    ///
    /// Example rows for a Savings product:
    ///   (SavingProductId, Deposits, Individual)    → LedgerNumber "2112001"
    ///   (SavingProductId, Deposits, Group)         → LedgerNumber "2113001"
    ///   (SavingProductId, InterestExpense, All)    → LedgerNumber "3121003"
    ///
    /// CostCentreCode / RevenueCentreCode link to AccountReferenceValue records
    /// in the Accounting module. When a transaction is posted from this product,
    /// these codes are written to Reference1/Reference2 on the GeneralLedgerEntry,
    /// enabling branch-level P&L reporting by product.
    /// </summary>
    public class ProductPostingAccount : BaseEntity {
        public long ProductId { get; set; }
        public ProductModuleType ProductModule { get; set; }
        public PostingPurpose PostingPurpose { get; set; }
        public CustomerSegment CustomerSegment { get; set; }

        /// <summary>GL ledger account number. References LedgerAccount.LedgerNumber.</summary>
        public string LedgerNumber { get; set; } = string.Empty;

        /// <summary>
        /// Optional cost centre code (AccountReferenceValue.Code where Reference.Code = "COST").
        /// Written to GeneralLedgerEntry.Reference1 when posting.
        /// </summary>
        public string? CostCentreCode { get; set; }

        /// <summary>
        /// Optional revenue centre code (AccountReferenceValue.Code where Reference.Code = "REVS").
        /// Written to GeneralLedgerEntry.Reference2 when posting.
        /// </summary>
        public string? RevenueCentreCode { get; set; }
    }

}
