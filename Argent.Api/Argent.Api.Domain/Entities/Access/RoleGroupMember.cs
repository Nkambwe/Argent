using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {
    /// <summary>
    /// Links a Role to a RoleGroup. A role can belong to multiple groups.
    /// </summary>
    public class RoleGroupMember : BaseEntity {
        public long RoleGroupId { get; set; }
        public RoleGroup RoleGroup { get; set; } = null!;
        public long RoleId { get; set; }
        public Role Role { get; set; } = null!;
    }
}
