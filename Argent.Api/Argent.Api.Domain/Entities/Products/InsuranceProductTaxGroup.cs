using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Taxes;

namespace Argent.Api.Domain.Entities.Products {
    public class InsuranceProductTaxGroup: BaseEntity {
        public long ProductId { get; set; }
        public virtual InsuranceProduct? Product { get; set; }

        public long TaxGroupId { get; set; }
        public virtual TaxGroup? TaxGroup { get; set; }
    }
}
