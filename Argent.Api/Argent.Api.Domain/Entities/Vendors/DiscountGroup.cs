using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Vendors {

    /// <summary>
    /// Discount group like High volume customer, Medium volume customer, low volume customer, High volume supplier, Medium volume supplier, Low volume supplier
    /// </summary>
    public class DiscountGroup : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string GroupName { get; set; } = string.Empty;
        public GroupType Type { get; set; }
        public string Notes { get; set; } = string.Empty;
        public virtual ICollection<PurchaseOrderDefault> PurchaseOrderDefaults { get; set; } = [];
    }
}
