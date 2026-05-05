using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Vendors;

namespace Argent.Api.Domain.Entities.Accounting.Taxes {
    public class VendorTax : BaseEntity {
        public long VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }
        public long TaxId { get; set; }
        public virtual Tax? Tax { get; set; }
        
    }

}
