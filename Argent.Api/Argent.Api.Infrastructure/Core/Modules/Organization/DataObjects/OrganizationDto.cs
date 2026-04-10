namespace Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects {
    public class OrganizationDto {
        public long Id { get; set; }
        public string RegisteredName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string BusinessLine { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<BranchDto> Branches { get; set; } = [];
    }
}
