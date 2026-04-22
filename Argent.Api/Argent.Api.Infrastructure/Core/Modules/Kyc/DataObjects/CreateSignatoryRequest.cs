namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class CreateSignatoryRequest {
        public string Name { get; set; } = string.Empty;
        public string? Position { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public bool CanSignAlone { get; set; }
        public string? Photo { get; set; }
        public string? Signature { get; set; }
        public string? Notes { get; set; }
    }
}
