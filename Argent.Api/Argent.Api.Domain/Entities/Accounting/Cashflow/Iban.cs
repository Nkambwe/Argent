using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// International Bank Account Number routing identifier.
    /// A single IBAN entry can be shared across multiple bank branches.
    /// </summary>
    public class Iban : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string? Narration { get; set; }
        public ICollection<Bank> Banks { get; set; } = [];
    }
}
