namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class AssignPermissionsRequest {
        /// <summary>
        /// Permissions to add — additive, existing permissions are preserved.
        /// </summary>
        public List<long> PermissionIds { get; set; } = [];
    }


}
