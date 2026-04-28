using Argent.Api.Domain.Entities.Access;

namespace Argent.Api.Infrastructure.Repositories.Access {

    /// <summary>
    /// Repository for Role entities.
    /// </summary>
    /// <remarks>
    /// Extends IBaseRepository&lt;Role&gt; for standard CRUD. Adds domain-specific queries for role management.
    /// </remarks>
    public interface IRoleRepository : IRepository<Role> {
        Task<Role?> GetByNameAsync(string name, CancellationToken token = default);
        Task<Role?> GetWithPermissionsAsync(long roleId, CancellationToken token = default);
        Task<IEnumerable<Role>> GetAllWithPermissionsAsync(CancellationToken token = default);
        Task<bool> NameExistsAsync(string name, long? excludeId = null, CancellationToken token = default);
        Task AddPermissionAsync(RolePermission rolePermission, CancellationToken token = default);
        Task RemovePermissionAsync(long roleId, long permissionId, CancellationToken token = default);
    }
}
