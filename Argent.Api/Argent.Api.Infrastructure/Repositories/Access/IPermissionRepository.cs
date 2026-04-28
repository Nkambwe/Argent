using Argent.Api.Domain.Entities.Access;

namespace Argent.Api.Infrastructure.Repositories.Access {
    /// <summary>
    /// Repository for Permission entities.
    /// Permissions are mostly read-only after seeding — management operations
    /// are infrequent and only available to System Administrators.
    /// </summary>
    public interface IPermissionRepository : IRepository<Permission> {
        Task<IEnumerable<Permission>> GetByModuleAsync(string module, CancellationToken ct = default);
        Task<Permission?> GetByNameAsync(string name, CancellationToken ct = default);
        Task<bool> NameExistsAsync(string name, CancellationToken ct = default);

        /// <summary>
        /// Returns the flat list of permission names assigned to a user
        /// through all their roles — the primary query for JWT token generation.
        /// </summary>
        Task<IReadOnlyList<string>> GetUserPermissionsAsync(long userId, CancellationToken ct = default);
    }
}
