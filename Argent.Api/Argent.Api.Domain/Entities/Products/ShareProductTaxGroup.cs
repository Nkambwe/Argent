using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Taxes;

namespace Argent.Api.Domain.Entities.Products {
    public class ShareProductTaxGroup:BaseEntity {
        public long ShareProductId { get; set; }
        public virtual ShareProduct? ShareProduct { get; set; }
        public long TaxGroupId { get; set; }
        public virtual TaxGroup? TaxGroup { get; set; }
    }
}
