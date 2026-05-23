using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// A rate entry for a time deposit product term.
    /// e.g. 3-month deposits earn 8.5%, 6-month earn 9%, 12-month earn 10%.
    /// </summary>
    public class TimedepositRate : BaseEntity {
        public long TimedepositProductId { get; set; }
        public TimedepositProduct TimedepositProduct { get; set; } = null!;

        /// <summary>Term length this rate applies to.</summary>
        public int Period { get; set; }
        public IntervalType PeriodType { get; set; } = IntervalType.Months;

        /// <summary>Annual interest rate for this term.</summary>
        public decimal InterestRate { get; set; }

        /// <summary>Minimum deposit amount to qualify for this rate.</summary>
        public decimal MinimumAmount { get; set; }

        public bool IsActive { get; set; } = true;
    }
}
