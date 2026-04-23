namespace Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects {
    public class AssignRolesToGroupRequest {
        /// <summary>
        /// Roles to add to this group. Existing roles are preserved.
        /// </summary>
        public List<long> RoleIds { get; set; } = [];
    }

}
