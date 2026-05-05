using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Vendors {
    /// <summary>
    /// Price group like Major Purchase, Major Sales, Wholesale Purchase, Wholesale Selling, Retail Purchase, Retail Selling, Inter company Purchase, Inter company Selling
    /// Domestic Suppliers, International Suppliers
    /// </summary>
    public class PriceGroup : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public GroupType Type { get; set; }
        public string Notes { get; set; } = string.Empty;
        public virtual ICollection<PurchaseOrderDefault> PurchaseOrderDefaults { get; set; } = [];
    }
}
