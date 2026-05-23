using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Taxes;

namespace Argent.Api.Domain.Entities.Products {
    public class InsuranceProductTaxGroup: BaseEntity {
        public long InsuranceProductId { get; set; }
        public virtual InsuranceProduct? InsuranceProduct { get; set; }

        public long TaxGroupId { get; set; }
        public virtual TaxGroup? TaxGroup { get; set; }
    }
}
