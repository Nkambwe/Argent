using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Vendors {
    public class VendorAddress : BaseEntity {

        [Encryptable("Address")]
        public string Address { get; set; } = string.Empty;
        public AddressFor For { get; set; }
        public bool IsPrimary { get; set; }
        public long? VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }

}
