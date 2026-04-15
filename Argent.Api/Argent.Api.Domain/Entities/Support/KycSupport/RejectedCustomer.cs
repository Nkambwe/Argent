using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Support.KycSupport {
    /// <summary>
    /// Records a customer registration rejection and the reason.
    /// </summary>
    public class RejectedCustomer : BaseEntity {
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public DateTime RejectDate { get; set; }
        public string RejectedBy { get; set; } = string.Empty;
        public long ReasonId { get; set; }
        public RejectReason Reason { get; set; } = null!;
        public string? Notes { get; set; }
    }

}
