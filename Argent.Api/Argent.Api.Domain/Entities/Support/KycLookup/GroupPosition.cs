using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Support.KycLookup {
    // ── Group Position ────────────────────────────────────────────────────────

    /// <summary>
    /// Role/position a member can hold within a group (e.g. Chairperson, Secretary, Treasurer).
    /// </summary>
    public class GroupPosition : BaseEntity {
        public string Series { get; set; } = string.Empty;
        public string Designation { get; set; } = string.Empty;
        public string? Notes { get; set; }
    }
}
