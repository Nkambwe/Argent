using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Postings;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting.Journals {
    /// <summary>
    /// Configuration template for a category of journal entries.
    /// Mirrors VoucherType but for journal-specific postings (not cash/bank movements).
    /// Controls which GL account, posting groups, and reference dimensions apply
    /// to journals of this type.
    ///
    /// AllowTaxDifference: permits small rounding differences in tax amounts.
    /// RequireVoucher: journal entries of this type must have an attached voucher.
    /// MultiCurrency: this journal type supports foreign currency postings.
    ///
    /// ReferenceValue1-6: default analytical dimension values pre-filled for this journal type.
    /// </summary>
    public class JournalType : BaseEntity {
        public string SeriesIdentifier { get; set; } = string.Empty;
        public string JournalName { get; set; } = string.Empty;
        /// <summary>
        /// Default GL ledger number for this journal type.
        /// </summary>
        public string? DefaultLedgerNumber { get; set; }
        public AccountClassification AccountClassification { get; set; }
        public bool AllowTaxDifference { get; set; }
        public bool RequireVoucher { get; set; }
        public bool MultiCurrency { get; set; }
        public bool IsSystem { get; set; }
        public bool IsActive { get; set; } = true;
        public long? GeneralPostingGroupId { get; set; }
        public GeneralPostingGroup? GeneralPostingGroup { get; set; }
        public long? BranchPostingGroupId { get; set; }
        public BranchPostingGroup? BranchPostingGroup { get; set; }
        public long? BusinessPostingGroupId { get; set; }
        public BusinessPostingGroup? BusinessPostingGroup { get; set; }
        public string? ReferenceValue1 { get; set; }
        public string? ReferenceValue2 { get; set; }
        public string? ReferenceValue3 { get; set; }
        public string? ReferenceValue4 { get; set; }
        public string? ReferenceValue5 { get; set; }
        public string? ReferenceValue6 { get; set; }
        public long? ReasonId { get; set; }

        public ICollection<JournalEntry> Journals { get; set; } = [];
        public ICollection<CashierJournalType> Cashiers { get; set; } = [];
        public ICollection<JournalTypeTaxGroup> TaxGroups { get; set; } = [];
    }
}
