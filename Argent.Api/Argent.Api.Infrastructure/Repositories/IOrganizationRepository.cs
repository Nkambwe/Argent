using Argent.Api.Domain.Entities;

namespace Argent.Api.Infrastructure.Repositories {

    public interface IOrganizationRepository : IRepository<Organization> {

        #region Organization

        /// <summary>
        /// Returns the single organization (one per deployment).
        /// </summary>
        Task<Organization?> GetOrganizationAsync(CancellationToken ct = default);
        Task<Organization?> GetWithBranchesAsync(long organizationId, CancellationToken token = default);
        Task<bool> RegistrationNumberExistsAsync(string registrationNumber, CancellationToken token = default);

        #endregion

        #region Branches

        Task<Branch?> GetBranchByIdAsync(long branchId, CancellationToken token = default);
        Task<IEnumerable<Branch>> GetBranchesAsync(CancellationToken ct = default);
        Task<IEnumerable<Branch>> GetBranchesByOrganizationAsync(long organizationId, CancellationToken token = default);
        Task<Branch?> GetDefaultBranchAsync(long organizationId, CancellationToken token = default);
        Task<bool> BranchCodeExistsAsync(string code, long? excludeId = null, CancellationToken ct = default);
        Task<bool> BranchNameExistsAsync(long organizationId, string branchName, CancellationToken token = default);
        Task AddBranchAsync(Branch branch, CancellationToken token = default);
        Task<bool> HasAnyBranchAsync(CancellationToken ct = default);
        void UpdateBranch(Branch branch);
        Task ClearDefaultBranchAsync(CancellationToken ct = default);
        /// <summary>
        /// Clears IsDefault on all branches of the org, then sets it on the target branch.
        /// </summary>
        /// <remarks>
        /// Call within BeginTransactionAsync or CommitAsync. 
        /// </remarks>
        Task ClearAndSetDefaultBranchAsync(long organizationId, long newDefaultBranchId, CancellationToken token = default);
        #endregion

        #region Holidays
        Task<BranchHoliday?> GetHolidayByIdAsync(long id, CancellationToken ct = default);
        Task AddHolidayAsync(BranchHoliday holiday, CancellationToken ct = default);
        void RemoveHoliday(BranchHoliday holiday);
        #endregion

        #region Status

        Task<bool> CanConnectAsync();
        Task<bool> IsInitlialized();

        #endregion
    }
}
