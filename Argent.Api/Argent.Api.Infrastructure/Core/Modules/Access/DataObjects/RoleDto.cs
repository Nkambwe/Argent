namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class RoleDto {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsSystemRole { get; set; }
        public int UserCount { get; set; }
        public IEnumerable<PermissionDto> Permissions { get; set; } = [];
    }

}
