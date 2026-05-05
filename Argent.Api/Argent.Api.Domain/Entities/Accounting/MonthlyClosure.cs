using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// Tracks the open/closed state of each accounting month within a financial year.
    /// Closed months reject new postings.
    /// </summary>
    public class MonthlyClosure : BaseEntity {
        public FinMonth Month { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime CloseDate { get; set; }
        public long FinancialYearId { get; set; }
        public FinancialYear FinancialYear { get; set; } = null!;
        public ICollection<GeneralLedgerEntry> Entries { get; set; } = [];
    }

}


