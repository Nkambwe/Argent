namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class UserSummaryDto {
        public long Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime? LastLoginOn { get; set; }
        public string DefaultBranchName { get; set; } = string.Empty;
        public IEnumerable<string> Roles { get; set; } = [];
    }

}
