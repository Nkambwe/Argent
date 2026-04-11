namespace Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects {
    public class CreateRoleRequest {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<long> PermissionIds { get; set; } = [];
    }
}
