using Argent.Api.Domain.Entities.Access;

namespace Argent.Api.Infrastructure.Repositories.Access {
    /// <summary>
    /// Repository for AppUser entities.
    /// </summary>
    /// <remarks>
    /// Handles user lookup, creation, and branch access management.
    /// Refresh token management lives here since tokens belong to the user aggregate.
    /// </remarks>
    public interface IUserRepository : IRepository<AppUser> {

        #region Lookups
        Task<AppUser?> GetByUsernameAsync(string username, CancellationToken token = default);
        Task<AppUser?> GetByEmailAsync(string email, CancellationToken token = default);
        Task<AppUser?> GetWithAccessAsync(long userId, CancellationToken token = default);
        Task<IEnumerable<AppUser>> GetAllWithBranchAsync(CancellationToken token = default);
        Task<bool> UsernameExistsAsync(string username, long? excludeId = null, CancellationToken token = default);
        Task<bool> EmailExistsAsync(string email, long? excludeId = null, CancellationToken token = default);
        #endregion

        #region Role assignment
        Task AddUserRoleAsync(UserRole userRole, CancellationToken token = default);
        Task AssignRoleToUserAsync(long userId, long roleId, CancellationToken token = default);
        Task<UserRole?> GetUserRoleAsync(long userId, long roleId, CancellationToken token = default);
        void RemoveUserRole(UserRole userRole);
        #endregion

        #region Branch access
        Task<IEnumerable<UserBranchAccess>> GetBranchAccessAsync(long userId, CancellationToken token = default);
        Task AddBranchAccessAsync(UserBranchAccess access, CancellationToken token = default);
        Task AssignBranchAccessAsync(long userId, long branchId, bool canPost, CancellationToken ct = default);
        Task<UserBranchAccess?> GetBranchAccessEntryAsync(long userId, long branchId, CancellationToken token = default);
        Task<AppUser?> GetByIdWithAccessAsync(long userId, CancellationToken ct = default);
        void UpdateBranchAccess(UserBranchAccess access);
        #endregion

        #region Refresh tokens
        Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct = default);
        Task AddRefreshTokenAsync(RefreshToken token, CancellationToken ct = default);
        void UpdateRefreshToken(RefreshToken token);
        #endregion

        #region Password history
        Task<IEnumerable<PasswordHistory>> GetPasswordHistoryAsync(long userId, int count, CancellationToken token = default);
        Task AddPasswordHistoryAsync(PasswordHistory history, CancellationToken token = default);
        #endregion
    }
}
