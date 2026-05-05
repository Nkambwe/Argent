using Argent.Api.Domain.Common;
using System.Diagnostics;

namespace Argent.Api.Domain.Entities.Vendors {
    public class VendorItemGroup : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string ItemGroup { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public virtual ICollection<PurchaseOrderDefault> PurchaseOrderDefaults { get; set; } = [];
    }
}
