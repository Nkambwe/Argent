using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting {
    public class RevenueCenter : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string CenterName { get; set; } = string.Empty;
        public bool Suspend { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public long? BranchId { get; set; }
        public virtual Branch? Branch { get; set; }

    }
}
