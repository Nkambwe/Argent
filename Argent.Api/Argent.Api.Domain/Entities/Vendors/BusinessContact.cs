using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Vendors {
    public class BusinessContact : BaseEntity {
        [Encryptable("Contact Person")]
        public string ContactPerson { get; set; } = string.Empty;

        [Encryptable("Business Title")]
        public string BusinessTitle { get; set; } = string.Empty;

        [Encryptable("Email Address")]
        public string EmailAddress { get; set; } = string.Empty;

        [Encryptable("Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;
        public long? VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }

}
