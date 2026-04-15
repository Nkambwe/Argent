using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Argent.Api.Domain.Entities.Kyc.KycIndividuals;
using Argent.Api.Domain.Entities.Support.KycLookup;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc.KycFiles {
    /// <summary>
    /// An identity document belonging to any customer type or a guarantor/signatory.
    /// </summary>
    public class Identification : BaseEntity {
        public string? FileUrl { get; set; }
        public DateTime IssuedOn { get; set; }
        public DateTime? ExpiresOn { get; set; }

        public long IdentityTypeId { get; set; }
        public IdentificationType IdentityType { get; set; } = null!;

        public long IssuerAuthorityId { get; set; }
        public IssuerAuthority IssuerAuthority { get; set; } = null!;

        /// <summary>
        /// Individual or Member ID
        /// </summary>
        public long? CustomerId { get; set; }      
        public CustomerType? CustomerType { get; set; }

        public long? SignatoryId { get; set; }
        public Signatory? Signatory { get; set; }

        public long? GuarantorId { get; set; }
        public Guarantor? Guarantor { get; set; }

        // Linked images (scanned pages)
        public ICollection<ImageFile> Images { get; set; } = [];

        public string? Notes { get; set; }
    }
}
