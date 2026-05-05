using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Documents;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// Controls the auto-numbering of every document type in the system:
    /// receipts, invoices, loan disbursements, member registrations, etc.
    ///
    /// Each document type can have multiple series (e.g. per branch, per year),
    /// with one marked as Default for auto-selection.
    ///
    /// Manual = user can override the generated number.
    /// </summary>
    public class SeriesNumber : BaseEntity {
        /// <summary>Short identifier linking to a document class — e.g. "RCP" for receipts.</summary>
        public string Identifier { get; set; } = string.Empty;

        public string? CustomSeries { get; set; }

        /// <summary>
        /// Generated prefix — e.g. "RCP-KLA-"
        /// </summary>
        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public long StartNumber { get; set; } = 1;
        public long EndNumber { get; set; } = 999999;

        public DateOnly StartsOn { get; set; }
        public DateTime? LastUsedAt { get; set; }
        public long LastNumber { get; set; }

        public bool IsDefault { get; set; }
        public bool AllowManualOverride { get; set; }

        /// <summary>
        /// Null = organization-wide. Set = branch-specific series.
        /// </summary>
        public long? BranchId { get; set; }
        public Branch? Branch { get; set; }

        public long DocumentTypeId { get; set; }
        public TransactionDocumentType DocumentType { get; set; } = null!;
    }
}
