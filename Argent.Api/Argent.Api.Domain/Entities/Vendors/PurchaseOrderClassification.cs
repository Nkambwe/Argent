using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Vendors {
    public class PurchaseOrderClassification : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public virtual ICollection<PurchaseOrderDefault> PurchaseOrderDefaults { get; set; } = [];
    }
}
