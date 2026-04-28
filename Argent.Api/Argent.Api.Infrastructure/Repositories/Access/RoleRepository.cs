using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Access {
    public class RoleRepository(AppDataContext context) : Repository<Role>(context), IRoleRepository {
        public async Task<Role?> GetByNameAsync(string name, CancellationToken token = default)
            => await _dbSet.FirstOrDefaultAsync(r => r.Name == name && !r.IsDeleted, token);

        public async Task<Role?> GetWithPermissionsAsync(long roleId, CancellationToken ct = default)
            => await _dbSet.Include(r => r.RolePermissions.Where(rp => !rp.IsDeleted))
                    .ThenInclude(rp => rp.Permission)
                    .FirstOrDefaultAsync(r => r.Id == roleId && !r.IsDeleted, ct);

        public async Task<IEnumerable<Role>> GetAllWithPermissionsAsync(CancellationToken token = default)
            => await _dbSet.Include(r => r.RolePermissions.Where(rp => !rp.IsDeleted))
                    .ThenInclude(rp => rp.Permission)
                    .Where(r => !r.IsDeleted)
                    .OrderBy(r => r.Name)
                    .ToListAsync(token);

        public async Task<bool> NameExistsAsync(string name, long? excludeId = null, CancellationToken ct = default)
            => await _dbSet.AnyAsync(r => r.Name == name && !r.IsDeleted && (excludeId == null || r.Id != excludeId.Value), ct);

        public async Task AddPermissionAsync(RolePermission rolePermission, CancellationToken ct = default)
            => await _context.RolePermissions.AddAsync(rolePermission, ct);

        public async Task RemovePermissionAsync(long roleId, long permissionId, CancellationToken ct = default) {
            var rp = await _context.RolePermissions
                .FirstOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId && !rp.IsDeleted, ct);

            if (rp is not null) {
                rp.IsDeleted = true;
                rp.DeletedOn = DateTime.UtcNow;
            }
        }
    }


}
