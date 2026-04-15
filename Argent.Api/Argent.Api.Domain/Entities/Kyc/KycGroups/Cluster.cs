using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Kyc.KycGroups {
    /// <summary>
    /// An optional sub-grouping within a Group. Controlled by SystemConfig EnableClusters.
    /// A Cluster can be treated as a separate transacting unit (ClustersAsGroups config).
    /// </summary>
    public class Cluster : BaseEntity {
        public string Series { get; set; } = string.Empty;
        public string ClusterName { get; set; } = string.Empty;
        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        public DateTime? ClosedOn { get; set; }
        public string? Area { get; set; }
        public bool Active { get; set; } = true;
        public string? CreditOfficer { get; set; }

        /// <summary>
        /// True if this cluster was dissolved into another cluster.
        /// </summary>
        public bool Merged { get; set; }

        /// <summary>
        /// Code of the cluster this was merged into (if Merged = true).
        /// </summary>
        public string? MergedToCode { get; set; }

        public string? Notes { get; set; }

        public long GroupId { get; set; }
        public Group Group { get; set; } = null!;

        public ICollection<ClusterMember> Members { get; set; } = [];
    }
}
