namespace Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects {
    public class CreateRoleGroupRequest {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        /// <summary>
        /// roles to assign immediately on creation.
        /// </summary>
        public List<long> RoleIds { get; set; } = [];
    }

}
