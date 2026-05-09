using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    public class LoanProductChargeItem : BaseEntity {

        public long LoanProductId { get; set; }
        public virtual LoanProduct LoanProduct { get; set; } = null!;
        public long ChargeItemId { get; set; }
        public virtual ChargeItem ChargeItem { get; set; } = null!;
    }

}
