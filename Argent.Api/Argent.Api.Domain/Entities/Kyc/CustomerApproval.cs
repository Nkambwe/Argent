using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc {
    /// <summary>
    /// Approval workflow record for customer registration.
    /// SystemConfig RequireClientApproval controls whether this workflow is active.
    /// SystemConfig CanApproveOwnRegistrations controls self-approval.
    /// </summary>
    public class CustomerApproval : BaseEntity {
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public ApprovalStatus Status { get; set; }
        public DateTime ActionedOn { get; set; }
        public string ActionedBy { get; set; } = string.Empty;
        public string? Comments { get; set; }
        public string? Notes { get; set; }
    }
}
