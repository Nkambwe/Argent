using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Taxes {
    public class TaxableItem : BaseEntity {
        public string ItemCode { get; set; } = string.Empty;
        public string Item { get; set; } = string.Empty;
        public bool Suspend { get; set; }
        public DateTime? Started { get; set; }
        public long TaxId { get; set; }
        public virtual Tax? Tax { get; set; }
        public long? TimedepositProductId { get; set; }
        public virtual TimedepositProduct? TimedepositProduct { get; set; }
        public long? InsuranceProductId { get; set; }
        public virtual InsuranceProduct? InsuranceProduct { get; set; }
        public long? ShareProductId { get; set; }
        public virtual ShareProduct? ShareProduct { get; set; }
        public long? SavingProductId { get; set; }
        public virtual SavingProduct? SavingProduct { get; set; }
        public long? LoanProductId { get; set; }
        public virtual LoanProduct? LoanProduct { get; set; }
    }
}
