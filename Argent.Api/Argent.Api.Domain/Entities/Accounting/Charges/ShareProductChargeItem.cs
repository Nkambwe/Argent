using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    public class ShareProductChargeItem : BaseEntity {

        public long ShareProductId { get; set; }
        public virtual ShareProduct ShareProduct { get; set; } = null!;
        public long ChargeItemId { get; set; }
        public virtual ChargeItem ChargeItem { get; set; } = null!;
    }

}
