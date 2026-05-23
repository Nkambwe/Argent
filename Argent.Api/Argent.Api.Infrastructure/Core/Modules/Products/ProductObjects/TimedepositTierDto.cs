namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class TimedepositTierDto {
        public long Id { get; set; }
        public decimal FromAmount { get; set; }
        public decimal? ToAmount { get; set; }
        public decimal Rate { get; set; }
        public bool IsActive { get; set; }
    }

}
