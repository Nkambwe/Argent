namespace Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects {
    public class OrganizationUpdateRequest {
        public string RegisteredName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string BusinessLine { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
    }
}
