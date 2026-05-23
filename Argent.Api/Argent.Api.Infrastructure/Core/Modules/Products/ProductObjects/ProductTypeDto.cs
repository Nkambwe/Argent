namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {

    public class ProductTypeDto {
        public long Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public int Series { get; set; }
        public bool IsActive { get; set; }
        public bool IsSystem { get; set; }
        public string? Description { get; set; }
    }

}
