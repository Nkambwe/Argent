namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class PostingAccountDto {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public string ProductModule { get; set; } = string.Empty;
        public string PostingPurpose { get; set; } = string.Empty;
        public string CustomerSegment { get; set; } = string.Empty;
        public string LedgerNumber { get; set; } = string.Empty;
        public string? CostCentreCode { get; set; }
        public string? RevenueCentreCode { get; set; }
    }

}
