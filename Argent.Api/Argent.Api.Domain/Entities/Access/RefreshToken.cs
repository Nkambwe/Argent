using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {
    public class RefreshToken : BaseEntity {
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresOn { get; set; }
        public bool IsRevoked { get; set; } = false;
        public string? ReplacedByToken { get; set; }
        public string? CreatedByIp { get; set; }
        public long UserId { get; set; }
        public AppUser User { get; set; } = null!;

    }
}
