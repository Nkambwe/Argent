using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Currencies {
    /// <summary>
    /// Physical denomination of a currency (notes and coins).
    /// Used in teller cash management and cash declaration forms.
    /// e.g. UGX → 50000, 20000, 10000, 5000, 1000, 500, 100, 50
    /// </summary>
    public class Denomination : BaseEntity {
        public long CurrencyId { get; set; }
        public Currency Currency { get; set; } = null!;
        // e.g. "Fifty Thousand"
        public string Name { get; set; } = string.Empty;
        // e.g. "50,000"
        public string? Symbol { get; set; }                    
        public decimal Value { get; set; }
    }

}
