using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {
    /// <summary>
    /// Junction table for Role and Permission objects
    /// </summary>
    public class RolePermission : BaseEntity {
        public long RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public long PermissionId { get; set; }
        public Permission Permission { get; set; } = null!;
    }
}
