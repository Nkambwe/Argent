using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Settings {
    public class SystemConfiguration : BaseEntity {
        /// <summary>
        /// Module name e.g. "Access", "Savings"
        /// </summary>
        public string Module { get; set; } = string.Empty;          
        /// <summary>
        /// Parameter key e.g. "PasswordMinLength"
        /// </summary>
        public string Key { get; set; } = string.Empty;
        /// <summary>
        /// Parameter value always stored as string
        /// </summary>
        public string Value { get; set; } = string.Empty;         
        public ConfigDataType DataType { get; set; }
        public string? Description { get; set; }
        /// <summary>
        /// Perameter editable state, set as false = read-only, set by code only
        /// </summary>
        public bool IsEditable { get; set; } = true;               
    }
}
