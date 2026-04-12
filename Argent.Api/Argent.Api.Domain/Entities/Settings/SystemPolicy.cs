using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Settings {
    /// <summary>
    /// A named policy that maps to a SystemConfiguration key.
    /// Policies are the enforceable rules derived from configuration values.
    /// </summary>
    /// <remarks>
    /// For example, Policy "AllowWeekendWork" maps to SystemConfiguration Access.AllowWeekendWork.
    /// A RoleGroupPolicyOverride can then say "Administrators can work weekends" even if the system default is false. 
    /// </remarks>
    public class SystemPolicy : BaseEntity {
        /// <summary>
        /// Policy name e.g. "AllowWeekendWork"
        /// </summary>
        public string Name { get; set; } = string.Empty;           
        public string Module { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Policy default value, it mirrors SystemConfiguration value
        /// </summary>
        public string DefaultValue { get; set; } = string.Empty;  
        public ConfigDataType DataType { get; set; }
        /// <summary>
        /// Policy overridable status, set as false = cannot be overridden at group level
        /// </summary>
        public bool IsOverridable { get; set; } = true;            

        public ICollection<RoleGroupPolicyOverride> Overrides { get; set; } = new List<RoleGroupPolicyOverride>();
    }
}
