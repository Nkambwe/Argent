using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {
    /// <summary>
    /// Explicitly grants a user access to a specific branch beyond their home branch.
    /// This is the basis for the branch access check performed on every request.
    /// </summary>
    public class UserBranchAccess : BaseEntity {
        public long UserId { get; set; }
        public AppUser User { get; set; } = null!;
        public long BranchId { get; set; }
        public Branch Branch { get; set; } = null!;

        /// <summary>
        /// If true, the user can switch their working context to this branch.
        /// Some users may view data from multiple branches but only post from one.
        /// </summary>
        public bool CanPost { get; set; } = true;
    }
}
