using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Settings;

namespace Argent.Api.Domain.Entities.Access {
    /// <summary>
    /// An organisational grouping of related roles. E.g. "Tellers" group containing "Teller" and "Teller Supervisor" roles.
    /// </summary>
    /// <remarks>
    /// RoleGroups are the target of SystemPolicyOverrides,you set policy
    /// exceptions at group level, not per individual role. 
    /// </remarks>
    public class RoleGroup : BaseEntity {
        /// <summary>
        /// Role Group Name e.g. "Tellers"
        /// </summary>
        public string Name { get; set; } = string.Empty;       
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<RoleGroupMember> Members { get; set; } = [];
        public ICollection<RoleGroupPolicyOverride> PolicyOverrides { get; set; } = [];
    }
}
