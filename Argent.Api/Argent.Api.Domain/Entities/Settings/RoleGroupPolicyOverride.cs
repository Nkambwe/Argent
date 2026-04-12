using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Access;

namespace Argent.Api.Domain.Entities.Settings {
    /// <summary>
    /// Overrides a SystemPolicy for a specific RoleGroup.
    /// </summary>
    /// <remarks>
    /// Resolution order from highest to lowest priority:
    ///   1. RoleGroupPolicyOverride if exists for user's role group
    ///   2. SystemPolicy.DefaultValue or SystemConfiguration value
    ///
    /// Example:
    ///   SystemPolicy "AllowWeekendWork" = false, we conside system default
    ///   RoleGroupPolicyOverride for "Administrators" group = true, Administrators can work on weekends; all other groups cannot.
    /// </remarks>
    public class RoleGroupPolicyOverride : BaseEntity {
        public long RoleGroupId { get; set; }
        public RoleGroup RoleGroup { get; set; } = null!;
        public long SystemPolicyId { get; set; }
        public SystemPolicy SystemPolicy { get; set; } = null!;
        /// <summary>
        /// Overridable value, e.g. "true"
        /// </summary>
        public string OverrideValue { get; set; } = string.Empty;
        /// <summary>
        /// Override reason, why this override exists
        /// </summary>
        public string? Reason { get; set; }                       
    }
}
