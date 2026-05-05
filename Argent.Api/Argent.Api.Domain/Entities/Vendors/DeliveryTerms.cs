using Argent.Api.Domain.Common;
using System.Diagnostics;

namespace Argent.Api.Domain.Entities.Vendors {
    public class DeliveryTerms : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<Vendor> Vendors { get; set; } = [];
    }
}
