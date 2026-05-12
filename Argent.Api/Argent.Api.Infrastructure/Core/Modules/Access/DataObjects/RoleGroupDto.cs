namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class RoleGroupDto {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; }
        public IEnumerable<RoleGroupMemberDto> Roles { get; set; } = [];
        public IEnumerable<PolicyOverrideDto> PolicyOverrides { get; set; } = [];
    }


}
