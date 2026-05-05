using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Taxes;

namespace Argent.Api.Domain.Entities.Products {
    public class LoanProductTaxGroup: BaseEntity {
        public long ProductId { get; set; }
        public virtual LoanProduct? Product { get; set; }
        public long TaxGroupId { get; set; }
        public virtual TaxGroup? TaxGroup { get; set; }
    }
}
