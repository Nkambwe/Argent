using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc {
    /// <summary>
    /// Records when a locked or suspended customer record is unlocked.
    /// </summary>
    public class UnlockedCustomer : BaseEntity {
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public DateTime UnlockedOn { get; set; }
        public string UnlockedBy { get; set; } = string.Empty;
        public long ReasonId { get; set; }
        public GeneralReason Reason { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
