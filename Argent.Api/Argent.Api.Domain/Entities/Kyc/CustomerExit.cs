using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc {

    /// <summary>
    /// Tracks why and when a customer left the organization.
    /// </summary>
    public class CustomerExit : BaseEntity {
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public DateTime ExitedOn { get; set; }
        public long ReasonId { get; set; }
        public GeneralReason Reason { get; set; } = null!;
        public string? Notes { get; set; }
    }

}
