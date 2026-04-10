namespace Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects {
    public class OrganizationCreateRequest {
        public string RegisteredName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string BusinessLine { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;

        /// <summary>
        /// The default branch must be provided when registering the organization.
        /// </summary>
        public BranchCreateRequest DefaultBranch { get; set; } = new();
    }
}
