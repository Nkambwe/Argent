namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class PermissionDto {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Module { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
