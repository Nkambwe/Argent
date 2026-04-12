using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {
    public class Role : BaseEntity {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsSystemRole { get; set; } = false;
        
        public ICollection<UserRole> UserRoles { get; set; } = [];
        public ICollection<RolePermission> RolePermissions { get; set; } = [];
        public ICollection<RoleGroupMember> RoleGroupMembers { get; set; } = [];
    }
}
