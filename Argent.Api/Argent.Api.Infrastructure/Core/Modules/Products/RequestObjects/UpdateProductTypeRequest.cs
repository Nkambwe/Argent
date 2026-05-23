namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class UpdateProductTypeRequest {
        public string Name { get; set; } = string.Empty;
        public int Series { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }
    }
}
