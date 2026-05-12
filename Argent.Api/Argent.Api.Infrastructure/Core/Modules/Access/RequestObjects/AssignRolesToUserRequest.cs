namespace Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects {
    public class AssignRolesToUserRequest {
        /// <summary>
        /// Roles to add: additive, existing roles are preserved.
        /// </summary>
        public List<long> RoleIds { get; set; } = [];
    }

}
