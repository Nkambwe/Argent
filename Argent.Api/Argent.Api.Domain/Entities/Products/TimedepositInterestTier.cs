using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// An amount-based interest tier for tiered time deposit products.
    /// e.g. Deposits between FromAmount and ToAmount earn Rate per annum.
    /// </summary>
    public class TimedepositInterestTier : BaseEntity {
        public long TimedepositProductId { get; set; }
        public TimedepositProduct TimedepositProduct { get; set; } = null!;

        public decimal FromAmount { get; set; }

        /// <summary>Null means no upper limit (top tier).</summary>
        public decimal? ToAmount { get; set; }

        public decimal Rate { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
