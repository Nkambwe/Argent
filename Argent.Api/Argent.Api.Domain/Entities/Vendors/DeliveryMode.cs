using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Vendors {
    public class DeliveryMode : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Vendor> Vendors { get; set; } = [];
    }

}
