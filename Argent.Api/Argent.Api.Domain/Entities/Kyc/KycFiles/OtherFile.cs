using Argent.Api.Domain.Entities.Kyc.KycBusinesses;

namespace Argent.Api.Domain.Entities.Kyc.KycFiles {
    public class OtherFile : FileAttachment {
        // Optional: files can also be attached to signatories
        public long? SignatoryId { get; set; }
        public Signatory? Signatory { get; set; }

        public ICollection<ImageFile> Images { get; set; } = [];
    }
}
