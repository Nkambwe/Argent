namespace Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects {
    public class BranchDto {
        public long Id { get; set; }
        public long OrganizationId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string? PostalAddress { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
