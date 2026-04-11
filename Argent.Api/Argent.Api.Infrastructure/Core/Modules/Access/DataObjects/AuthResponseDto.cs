namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class AuthResponseDto {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public long UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public long DefaultBranchId { get; set; }
        public IReadOnlyList<string> Permissions { get; set; } = [];
        public IReadOnlyList<BranchAccessDto> AccessibleBranches { get; set; } = [];
    }

}
