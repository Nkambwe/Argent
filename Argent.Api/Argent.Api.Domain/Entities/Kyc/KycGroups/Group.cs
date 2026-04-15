using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Support.KycLookup;

namespace Argent.Api.Domain.Entities.Kyc.KycGroups {
    /// <summary>
    /// A group of members who collectively transact.
    /// </summary>
    public class Group : CustomerBase {
        [Encryptable("Group Name")]
        public string RegisteredName { get; set; } = string.Empty;
        public long? GroupFilter1Id { get; set; }
        public CustomerFilter? GroupFilter1 { get; set; }

        public long? GroupFilter2Id { get; set; }
        public CustomerFilter? GroupFilter2 { get; set; }
        public ICollection<Member> Members { get; set; } = [];
        public ICollection<Cluster> Clusters { get; set; } = [];
        public ICollection<Meeting> Meetings { get; set; } = [];
    }
}
