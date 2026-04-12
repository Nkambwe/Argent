using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities {
    /// <summary>
    /// A physical or logical branch of the Organization.
    /// </summary>
    public class Branch : BaseEntity {
        public long OrganizationId { get; set; }
        public string BranchCode { get; set; } = string.Empty;
        public string BranchName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string? PostalAddress { get; set; }

        /// <summary>
        /// Marks the head-office or primary branch.
        /// </summary>
        /// <remarks>
        /// Only one branch per organization can be default, this is enforced at DB level.
        /// Used as fallback for routing, default ledger accounts, and config resolution.
        /// </remarks>
        public bool IsDefault { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public Organization Organization { get; set; } = null!;
        public ICollection<BranchHoliday> Holidays { get; set; } = [];
    }
}
