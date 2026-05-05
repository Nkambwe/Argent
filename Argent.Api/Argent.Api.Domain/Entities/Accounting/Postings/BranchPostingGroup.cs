using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Postings {
    /// <summary>
    /// Branch-level posting group.
    /// Controls which cost centre / branch accounts transactions post to
    /// when originating from a specific branch type.
    /// </summary>
    public class BranchPostingGroup : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }

        /*Branch GL account mappings*/
        public string? ReceivablesAccountNumber { get; set; }
        public string? PayablesAccountNumber { get; set; }
    }
}
