using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    public class InsuranceProductChargeItem : BaseEntity {

        public long InsuranceProductId { get; set; }
        public virtual InsuranceProduct InsuranceProduct { get; set; } = null!;
        public long ChargeItemId { get; set; }
        public virtual ChargeItem ChargeItem { get; set; } = null!;
    }

}
