using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {
    /// <summary>
    /// A granular action a user can perform.
    /// Format: "{Module}.{Action}" e.g. "CustomerKyc.Create", "Savings.Approve", "Shares.ViewDividends"
    /// </summary>
    public class Permission : BaseEntity {
        public string Name { get; set; } = string.Empty;       
        public string Module { get; set; } = string.Empty;     
        public string Action { get; set; } = string.Empty;     
        public string? Description { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; } = [];
    }
}
