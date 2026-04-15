namespace Argent.Api.Domain.Entities.Kyc.KycFiles {
    public class TitleDeed : FileAttachment {
        public string? PlotNumber { get; set; }
        public string? Block { get; set; }
        public string? Location { get; set; }
        public ICollection<ImageFile> Images { get; set; } = [];
    }
}
