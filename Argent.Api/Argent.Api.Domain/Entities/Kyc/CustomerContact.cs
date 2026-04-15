using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc {
    /// <summary>
    /// Next of kin or emergency contact for any customer type.
    /// </summary>
    public class CustomerContact : BaseEntity {
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public string Series { get; set; } = string.Empty;
        public string ContactName { get; set; } = string.Empty;
        public string? Telephone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Relationship { get; set; }
        public string? Notes { get; set; }
    }
}
