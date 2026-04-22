namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class CreateContactRequest {
        public string ContactName { get; set; } = string.Empty;
        public string? Telephone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Relationship { get; set; }
        public string? Notes { get; set; }
    }
}
