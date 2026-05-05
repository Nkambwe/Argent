using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Vendors {
    public class PaymentTerm : BaseEntity {
        public string Terms { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public virtual ICollection<PaymentDefault> PaymentDefaults { get; set; } = [];
    }

}
