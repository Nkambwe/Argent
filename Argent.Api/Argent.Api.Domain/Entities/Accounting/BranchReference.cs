using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting {
    public class BranchReference : BaseEntity {
        public string Series { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool Suspend { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? BranchId { get; set; }
        public virtual Branch? Branch { get; set; }
        public long ReferenceValueId { get; set; }
        public virtual AccountReferenceValue? ReferenceValue { get; set; }
    }
}
