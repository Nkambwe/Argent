namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class ShareProductSummaryDto {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public decimal NominalValue { get; set; }
        public string DividendMethod { get; set; } = string.Empty;
    }

}
