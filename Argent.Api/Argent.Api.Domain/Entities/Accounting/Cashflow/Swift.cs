using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// Society for Worldwide Interbank Financial Telecommunication code.
    /// Identifies banks internationally for wire transfers.
    /// </summary>
    public class Swift : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ICollection<Bank> Banks { get; set; } = [];
    }
}
