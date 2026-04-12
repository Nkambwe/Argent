using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {
    public class PasswordHistory : BaseEntity {
        public long UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime ChangedOn { get; set; } = DateTime.UtcNow;
    }
}
