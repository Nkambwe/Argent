namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class TimedepositProductSummaryDto {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public int Period { get; set; }
        public string PeriodType { get; set; } = string.Empty;
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public bool TierInterest { get; set; }
    }

}
