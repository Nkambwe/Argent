using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Support.KycLookup {
    /// <summary>
    /// The authority that issues an identification document (e.g. Uganda NIRA, DCIC).
    /// </summary>
    public class IssuerAuthority : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
