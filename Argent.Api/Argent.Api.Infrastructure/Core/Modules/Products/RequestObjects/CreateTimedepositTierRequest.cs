namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class CreateTimedepositTierRequest {
        public decimal FromAmount { get; set; }
        public decimal? ToAmount { get; set; }
        public decimal Rate { get; set; }
    }
}
