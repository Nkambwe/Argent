using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Taxes;

namespace Argent.Api.Domain.Entities.Products {
    public class TimedepositProductTaxGroup: BaseEntity {
        public long TimedepositProductId { get; set; }
        public virtual TimedepositProduct? TimedepositProduct { get; set; }
        public long TaxGroupId { get; set; }
        public virtual TaxGroup? TaxGroup { get; set; }
    }
}
