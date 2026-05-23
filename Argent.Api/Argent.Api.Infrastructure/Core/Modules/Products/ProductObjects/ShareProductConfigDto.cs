namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class ShareProductConfigDto {
        public decimal NominalValue { get; set; }
        public decimal MinimumShareCapital { get; set; }
        public string DividendCalculationMethod { get; set; } = string.Empty;
        public int DividendCalculationPeriod { get; set; }
        public string DividendCalculationInterval { get; set; } = string.Empty;
        public decimal DividendRate { get; set; }
        public bool ChargeWithholdingTaxOnDividends { get; set; }
        public bool AllowShareRedemption { get; set; }
        public int MinimumSharesAfterRedemption { get; set; }
        public bool RequireApprovalForRedemption { get; set; }
    }

}
