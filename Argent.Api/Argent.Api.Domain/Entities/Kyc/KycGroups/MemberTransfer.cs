using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Support.KycSupport;

namespace Argent.Api.Domain.Entities.Kyc.KycGroups {
    /// <summary>
    /// Records the transfer of a member from one group (or cluster) to another.
    /// </summary>
    public class MemberTransfer : BaseEntity {
        public long MemberId { get; set; }
        public Member Member { get; set; } = null!;

        /// <summary>
        /// If true, transfer is within clusters of the same group.
        /// </summary>
        public bool IsClusterTransfer { get; set; }

        public string? FromGroupCode { get; set; }
        public string? FromMemberCode { get; set; }
        public string? ToGroupCode { get; set; }
        public string? ToMemberCode { get; set; }
        public DateTime TransferDate { get; set; }
        public bool Approved { get; set; }
        public long ReasonId { get; set; }
        public GeneralReason Reason { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
