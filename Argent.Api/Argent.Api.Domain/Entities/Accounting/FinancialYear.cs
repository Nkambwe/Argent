using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// Defines the organization's financial year and its open/closed state.
    /// Each branch can have its own financial year to support staggered closures.
    /// </summary>
    public class FinancialYear : BaseEntity {
        /// <summary>e.g. "YR2024", "FY25"</summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>e.g. "2024", "2024-2025"</summary>
        public string YearName { get; set; } = string.Empty;

        /// <summary>Number of accounting periods (12 or 13).</summary>
        public Period Period { get; set; }

        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }

        /// <summary>When true, no new postings are allowed in this year.</summary>
        public bool Closed { get; set; }

        /// <summary>
        /// Null = applies to all branches.
        /// Set = applies only to this branch (for staggered year-end).
        /// </summary>
        public long? BranchId { get; set; }
        public Branch? Branch { get; set; }

        public ICollection<MonthlyClosure> MonthlyClosures { get; set; } = [];
    }
}


