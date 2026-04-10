using Argent.Api.Domain.Entities;

namespace Argent.Api.Infrastructure.Repositories {

    public interface IOrganizationRepository : IRepository<Organization> {
        Task<Organization?> GetWithBranchesAsync(long organizationId, CancellationToken token = default);
        Task<bool> RegistrationNumberExistsAsync(string registrationNumber, CancellationToken token = default);
        Task<Branch?> GetBranchByIdAsync(long branchId, CancellationToken token = default);
        Task<IEnumerable<Branch>> GetBranchesByOrganizationAsync(long organizationId, CancellationToken token = default);
        Task<Branch?> GetDefaultBranchAsync(long organizationId, CancellationToken token = default);
        Task<bool> BranchNameExistsAsync(long organizationId, string branchName, CancellationToken token = default);
        Task AddBranchAsync(Branch branch, CancellationToken token = default);
        void UpdateBranch(Branch branch);

        /// <summary>
        /// Clears IsDefault on all branches of the org, then sets it on the target branch.
        /// </summary>
        /// <remarks>
        /// Call within BeginTransactionAsync or CommitAsync. 
        /// </remarks>
        Task ClearAndSetDefaultBranchAsync(long organizationId, long newDefaultBranchId, CancellationToken token = default);
        Task<bool> CanConnectAsync();
        Task<bool> IsInitlialized();
    }

}
