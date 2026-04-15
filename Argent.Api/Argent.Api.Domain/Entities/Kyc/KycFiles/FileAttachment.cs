using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc.KycFiles {
    /// <summary>
    /// Abstract base for all file/document entities attached to customers.
    /// All file types share a series code, file URL, type, and notes.
    /// </summary>
    public abstract class FileAttachment : BaseEntity {
        public string Series { get; set; } = string.Empty;
        public string FileUrl { get; set; } = string.Empty;
        public AttachmentType FileType { get; set; }
        public string? Notes { get; set; }
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
    }
}
