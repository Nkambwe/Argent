using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {
    /// <summary>
    /// Class representing a collection of related roles
    /// </summary>
    public class RoleGroup : BaseEntity {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsSystemGroup { get; set; } = false;

        public ICollection<Role> Roles { get; set; } = [];
    }
}
