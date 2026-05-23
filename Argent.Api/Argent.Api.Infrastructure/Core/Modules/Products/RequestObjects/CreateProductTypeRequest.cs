using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class CreateProductTypeRequest {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ProductModuleType Module { get; set; }
        public int Series { get; set; }
        public string? Description { get; set; }
    }
}
