using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Vendors;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A specific value within an account reference dimension.
    /// e.g. Reference "Department" → Value "Finance"
    /// </summary>
    public class AccountReferenceValue : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Suspended { get; set; }
        public DateOnly? Start { get; set; }
        public DateOnly? End { get; set; }

        /// <summary>
        /// When false, only system-generated entries can use this value.
        /// </summary>
        public bool AllowManualEntry { get; set; } = true;

        public long ReferenceId { get; set; }
        public AccountReference AccountReference { get; set; } = null!;

        public ICollection<BranchReference> BranchReferences { get; set; } = [];
        public ICollection<VendorReference> VendorReferences { get; set; } = [];
    }
}


