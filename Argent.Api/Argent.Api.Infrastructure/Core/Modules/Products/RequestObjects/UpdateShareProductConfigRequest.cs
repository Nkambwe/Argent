using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class UpdateShareProductConfigRequest {
        public decimal NominalValue { get; set; }
        public decimal MinimumShareCapital { get; set; }
        public DividendCalculationMethod DividendCalculationMethod { get; set; }
        public int DividendCalculationPeriod { get; set; }
        public IntervalType DividendCalculationInterval { get; set; }
        public decimal DividendRate { get; set; }
        public string DividendEarningShares { get; set; } = string.Empty;
        public bool ChargeWithholdingTaxOnDividends { get; set; }
        public bool AllowShareRedemption { get; set; }
        public int MinimumSharesAfterRedemption { get; set; }
        public bool RequireApprovalForRedemption { get; set; }
    }
}
