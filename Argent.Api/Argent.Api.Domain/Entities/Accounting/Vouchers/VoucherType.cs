using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Postings;
using Argent.Api.Domain.Entities.Support;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting.Vouchers {

    /// <summary>
    /// Configuration template for a category of voucher postings.
    /// Defines the default GL account, posting groups, and reference dimensions
    /// for payment vouchers, receipt vouchers, and journal vouchers.
    /// </summary>
    /// <remarks>
    /// System = true: created by the system, cannot be deleted.
    /// SeriesNumber: references the SeriesNumber engine for auto-numbering.
    /// Posting groups drive how double-entry lines are auto-generated. 
    /// </remarks>
    public class VoucherType : BaseEntity {
        public string SeriesIdentifier { get; set; } = string.Empty;
        public string VoucherName { get; set; } = string.Empty;
        /// <summary>
        /// Default GL ledger number this voucher type posts to.
        /// </summary>
        public string? DefaultLedgerNumber { get; set; }
        public PostingType Posting { get; set; }
        public VoucherCategory Category { get; set; }
        public bool IsSystem { get; set; }
        public bool IsActive { get; set; } = true;
        public long? GeneralPostingGroupId { get; set; }
        public GeneralPostingGroup? GeneralPostingGroup { get; set; }
        public long? BranchPostingGroupId { get; set; }
        public BranchPostingGroup? BranchPostingGroup { get; set; }
        public long? BusinessPostingGroupId { get; set; }
        public BusinessPostingGroup? BusinessPostingGroup { get; set; }
        public long? ReasonId { get; set; }
        public GeneralReason? Reason { get; set; }  
        public ICollection<VoucherLine> Vouchers { get; set; } = [];
        public ICollection<CashierVoucherType> Cashiers { get; set; } = [];
    }
}
