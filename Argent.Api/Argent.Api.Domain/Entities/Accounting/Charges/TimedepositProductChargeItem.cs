using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    public class TimedepositProductChargeItem: BaseEntity {
        public long TimedepositProductId { get; set; }
        public virtual TimedepositProduct TimedepositProduct { get; set; } = null!;
        public long ChargeItemId { get; set; }
        public virtual ChargeItem ChargeItem { get; set; } = null!;
    }

}
