namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class SignatoryDto {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Position { get; set; }
        public string? Mobile { get; set; }
        public bool CanSignAlone { get; set; }
        public bool Suspended { get; set; }
    }
}
