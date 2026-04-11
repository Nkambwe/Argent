namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class UserDto {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => !string.IsNullOrWhiteSpace(MiddleName)? $"{FirstName} {MiddleName} {LastName}" : $"{FirstName} {LastName}";
        public string? PhoneNumber { get; set; }
        public long DefaultBranchId { get; set; }
        public string DefaultBranchName { get; set; } = string.Empty;
        public IEnumerable<string> Permissions { get; set; } = [];
        public IEnumerable<string> Roles { get; set; } = [];
        public IEnumerable<BranchAccessDto> BranchAccess { get; set; } = [];
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? LastLoginOn { get; set; }
    }
}
