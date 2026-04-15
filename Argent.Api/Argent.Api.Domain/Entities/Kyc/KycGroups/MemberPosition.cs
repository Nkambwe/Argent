using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Support.KycLookup;

namespace Argent.Api.Domain.Entities.Kyc.KycGroups {
    /// <summary>
    /// Records a member's position/role within their group over time.
    /// </summary>
    public class MemberPosition : BaseEntity {
        public long MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public long PositionId { get; set; }
        public GroupPosition Position { get; set; } = null!;
        public DateTime Started { get; set; }
        public DateTime? Ended { get; set; }
        public string? Notes { get; set; }
    }

}
