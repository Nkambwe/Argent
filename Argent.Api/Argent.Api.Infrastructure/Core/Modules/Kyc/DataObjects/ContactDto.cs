namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    /// <summary>
    /// Customer contact object
    /// </summary>
    public class ContactDto {
        public long Id { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public string? Telephone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Relationship { get; set; }
    }
}
