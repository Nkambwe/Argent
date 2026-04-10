using Argent.Api.Domain.Entities.Access;

namespace Argent.Api.Infrastructure.Repositories.Access {
    public interface IAccessRepository {
        Task<AppUser?> GetUserByUsernameAsync(string username, CancellationToken token = default);
        Task<AppUser?> GetUserByIdAsync(long userId, CancellationToken token = default);
        Task<AppUser?> GetUserWithAccessAsync(long userId, CancellationToken token = default);
        Task<bool> UsernameExistsAsync(string username, CancellationToken token = default);
        Task<bool> EmailExistsAsync(string email, CancellationToken token = default);
        Task AddUserAsync(AppUser user, CancellationToken token = default);
        void UpdateUser(AppUser user);
        Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken token = default);
        Task<Role?> GetRoleByNameAsync(string name, CancellationToken token = default);
        Task AddRoleAsync(Role role, CancellationToken token = default);
        Task<IEnumerable<Permission>> GetPermissionsByUserAsync(long userId, CancellationToken token = default);
        Task<Permission?> GetPermissionByNameAsync(string name, CancellationToken token = default);
        Task AddPermissionAsync(Permission permission, CancellationToken token = default);
        Task SeedPermissionsAsync(IEnumerable<Permission> permissions, CancellationToken token = default);
        Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken cToken = default);
        Task AddRefreshTokenAsync(RefreshToken token, CancellationToken cToken = default);
        void UpdateRefreshToken(RefreshToken token);
        Task AssignRoleToUserAsync(long userId, long roleId, CancellationToken token = default);
        Task AssignPermissionToRoleAsync(long roleId, long permissionId, CancellationToken token = default);
        Task AssignBranchAccessAsync(long userId, long branchId, bool canPost, CancellationToken token = default);
    }

}
