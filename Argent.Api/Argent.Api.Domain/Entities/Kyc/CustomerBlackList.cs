using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc {

    /// <summary>
    /// Records when a customer is blacklisted and optionally un-blacklisted.
    /// </summary>
    public class CustomerBlackList : BaseEntity {
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public DateTime ListedOn { get; set; }
        public DateTime? UnListedOn { get; set; }
        public long ReasonId { get; set; }
        public GeneralReason Reason { get; set; } = null!;
        public string? Notes { get; set; }
    }

}
