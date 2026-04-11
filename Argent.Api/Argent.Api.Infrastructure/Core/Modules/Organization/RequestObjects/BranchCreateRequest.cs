namespace Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects {
    public class BranchCreateRequest {
        public string BranchCode { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string? PostalAddress { get; set; }
    }
}
