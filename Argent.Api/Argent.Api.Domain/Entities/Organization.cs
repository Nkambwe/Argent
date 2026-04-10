
using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities {
    /// <summary>
    /// Top-level tenant entity. 
    /// </summary>
    /// <remarks>
    /// There is typically one Organization per deployment, but the schema supports multi-tenancy if ever needed.
    /// </remarks>
    public class Organization : BaseEntity {
        public string RegisteredName { get; set; } = string.Empty;
        public string ShortName { get; set; } = string.Empty;
        public string RegistrationNumber { get; set; } = string.Empty;
        public string BusinessLine { get; set; } = string.Empty;
        public string ContactEmail { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public ICollection<Branch> Branches { get; set; } = [];
    }
}
