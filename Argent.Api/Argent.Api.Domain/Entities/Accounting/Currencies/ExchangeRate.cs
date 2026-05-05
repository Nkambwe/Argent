using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Currencies {
    /// <summary>
    /// Exchange rate of a currency against another at a point in time.
    /// IsRunning marks the current active rate for the period.
    ///
    /// Buy / Sale / Average: three rates for different transaction directions.
    /// Average is the organizational rate used for internal revaluation.
    /// </summary>
    public class ExchangeRate : BaseEntity {
        public long CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;

        /// <summary>
        /// Currency code being converted against — e.g. "USD" when CurrencyId is UGX.
        /// </summary>
        public string Against { get; set; } = string.Empty;

        public decimal Buy { get; set; }
        public decimal Sale { get; set; }

        /// <summary>
        /// Organization's stable rate for the period (used in revaluation).
        /// </summary>
        public decimal Average { get; set; }

        /// <summary>
        /// The currently active rate. Only one rate per currency-pair should be running.
        /// </summary>
        public bool IsRunning { get; set; }

        public DateTime EffectiveFrom { get; set; } = DateTime.UtcNow;
        public DateTime? EffectiveTo { get; set; }
    }

}
