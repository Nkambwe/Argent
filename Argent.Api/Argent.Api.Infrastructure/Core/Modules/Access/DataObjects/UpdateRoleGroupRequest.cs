namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class UpdateRoleGroupRequest {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
    }


}
