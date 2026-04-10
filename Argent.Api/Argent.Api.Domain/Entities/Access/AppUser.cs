using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Access {

    public class AppUser : BaseEntity {
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime? LastLoginAt { get; set; }
        public int FailedLoginAttempts { get; set; } = 0;
        public DateTime? LockedUntil { get; set; }
        /// <summary>
        /// The branch this user is primarily attached to.
        /// </summary>
        public long DefaultBranchId { get; set; }
        public Branch DefaultBranch { get; set; } = null!;
        public ICollection<UserRole> UserRoles { get; set; } = [];
        public ICollection<UserBranchAccess> BranchAccess { get; set; } = [];
        public ICollection<RefreshToken> RefreshTokens { get; set; } = [];

    }
}
