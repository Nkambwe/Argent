namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class TimedepositRateDto {
        public long Id { get; set; }
        public int Period { get; set; }
        public string PeriodType { get; set; } = string.Empty;
        public decimal InterestRate { get; set; }
        public decimal MinimumAmount { get; set; }
        public bool IsActive { get; set; }
    }

}
