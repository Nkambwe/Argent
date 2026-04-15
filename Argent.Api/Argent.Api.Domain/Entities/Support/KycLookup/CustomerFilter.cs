using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Support.KycLookup {

    /// <summary>
    /// Consolidated configurable classification filter.
    /// All customized KYC filters for individual, Groups, Group members and Business
    ///
    /// FilterScope and SlotNumber together identify which "filter field" this entry belongs to:
    ///   FilterScope.General and Slot 1 → ClientFilter1
    ///   FilterScope.General and Slot 2 → ClientFilter2
    ///   FilterScope.General and Slot 3 → ClientFilter3
    ///   FilterScope.Group   and Slot 1 → GroupFilter1
    ///   FilterScope.Member  and Slot 1 → MemberFilter1
    ///   etc.
    /// Labels (e.g. "Custom Filter 1") come from SystemConfig.
    /// </summary>
    public class CustomerFilter : BaseEntity {
        public FilterScope Scope { get; set; }
        /// <summary>
        /// Slot number 1, 2, or 3
        /// </summary>
        public int SlotNumber { get; set; }        
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string? Notes { get; set; }
    }
}
