namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class InsuranceProductSummaryDto {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int PolicyPeriod { get; set; }
        public decimal MonthlyPremium { get; set; }
    }

}
