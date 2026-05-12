namespace Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects {
    public class UpdateOrganizationRequest {
        public string RegistrationNumber { get; set; } = string.Empty;
        public string RegisteredName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string BusinessLine { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public bool IsActive { get; set; } = false;
    }
}
