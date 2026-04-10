namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class UserDto {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string FullName => $"{FirstName} {LastName}";
        public string? PhoneNumber { get; set; }
        public long DefaultBranchId { get; set; }
        public string HomeBranchName { get; set; } = string.Empty;
        public IEnumerable<string> Permissions { get; set; } = [];
        public IEnumerable<BranchAccessDto> BranchAccess { get; set; } = [];
        public bool IsActive { get; set; }
        public DateTime? LastLoginAt { get; set; }
    }
}
