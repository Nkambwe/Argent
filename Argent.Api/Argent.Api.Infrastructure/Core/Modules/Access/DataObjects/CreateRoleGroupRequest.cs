namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class CreateRoleGroupRequest {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public List<long> RoleIds { get; set; } = [];
    }


}
