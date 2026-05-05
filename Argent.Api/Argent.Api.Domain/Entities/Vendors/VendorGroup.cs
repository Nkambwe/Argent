using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Vendors {
    public class VendorGroup : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public virtual ICollection<Vendor> Vendors { get; set; } = [];
        public virtual ICollection<PurchaseOrderDefault> PurchaseOrderDefaults { get; set; } = [];

    }
}
