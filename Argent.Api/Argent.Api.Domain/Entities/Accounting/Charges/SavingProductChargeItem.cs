using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    public class SavingProductChargeItem : BaseEntity {

        public long SavingProductId { get; set; }
        public virtual SavingProduct SavingProduct { get; set; } = null!;
        public long ChargeItemId { get; set; }
        public virtual ChargeItem ChargeItem { get; set; } = null!;
    }

}
