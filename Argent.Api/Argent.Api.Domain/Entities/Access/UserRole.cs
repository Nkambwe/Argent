using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {
    /// <summary>
    /// Junction table between AppUser and Role
    /// </summary>
    public class UserRole : BaseEntity {
        public long UserId { get; set; }
        public AppUser User { get; set; } = null!;

        public long RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
