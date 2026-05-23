namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class SavingProductSummaryDto {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string ProductTypeName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool OfferInterest { get; set; }
        public decimal InterestRate { get; set; }
        public bool AllowOverdraft { get; set; }
        public decimal MinimumBalance { get; set; }
    }

}
