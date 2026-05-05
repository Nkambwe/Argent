using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Products {
    public abstract class ProductBase : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
    }
}
