using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Banking.Savings;

namespace Argent.Api.Domain.Entities.Kyc.KycFiles {
    /// <summary>
    /// Individual image pages/scans linked to OtherFile, TitleDeed, or Identification.
    /// </summary>
    public class ImageFile : BaseEntity {
        public string FileUrl { get; set; } = string.Empty;
        public string? Notes { get; set; }

        // Optional link to parent document,only one should be set
        public long? OtherFileId { get; set; }
        public OtherFile? OtherFile { get; set; }

        public long? TitleDeedId { get; set; }
        public TitleDeed? TitleDeed { get; set; }

        public long? IdentificationId { get; set; }
        public Identification? Identification { get; set; }

        public long? SavingPartnerId { get; set; }
        public SavingPartner? SavingPartner { get; set; }
    }
}
