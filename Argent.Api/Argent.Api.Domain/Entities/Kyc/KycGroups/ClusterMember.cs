using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Kyc.KycGroups {
    /// <summary>
    /// Links a Member to a Cluster within their Group.
    /// A Member can appear in at most one active Cluster at a time.
    /// </summary>
    public class ClusterMember : BaseEntity {
        public long ClusterId { get; set; }
        public Cluster Cluster { get; set; } = null!;
        public long MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public DateTime JoinedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ExitedOn { get; set; }
        public string? Notes { get; set; }
    }
}
