using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Support.KycSupport {
    /// <summary>
    /// Reasons specifically for customer registration rejection.
    /// </summary>
    public class RejectReason : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }

}
