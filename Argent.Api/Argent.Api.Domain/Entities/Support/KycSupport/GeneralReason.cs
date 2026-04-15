using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Support.KycSupport {

    /// <summary>
    /// General purpose reason lookup (exit, unlock, transfer, etc.).
    /// </summary>
    public class GeneralReason : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;   // e.g. "Exit", "Blacklist", "Unlock"
        public string? Notes { get; set; }
    }

}
