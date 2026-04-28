using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Access {
    public class PermissionRepository(AppDataContext context) 
        : Repository<Permission>(context), 
        IPermissionRepository {

        public async Task<IEnumerable<Permission>> GetByModuleAsync(string module, CancellationToken token = default)
            => await _dbSet.Where(p => p.Module == module && !p.IsDeleted)
                .OrderBy(p => p.Action).ToListAsync(token);

        public async Task<Permission?> GetByNameAsync(string name, CancellationToken token = default)
            => await _dbSet.FirstOrDefaultAsync(p => p.Name == name && !p.IsDeleted, token);

        public async Task<bool> NameExistsAsync(string name, CancellationToken token = default)
            => await _dbSet.AnyAsync(p => p.Name == name && !p.IsDeleted, token);

        public async Task<IReadOnlyList<string>> GetUserPermissionsAsync(long userId, CancellationToken ct = default) {
            var permissions = await _context.UserRoles
                .Where(ur => ur.UserId == userId && !ur.IsDeleted)
                .SelectMany(ur => ur.Role.RolePermissions
                .Where(rp => !rp.IsDeleted)
                .Select(rp => rp.Permission.Name))
                .Distinct()
                .ToListAsync(ct);

            return permissions.AsReadOnly();
        }
    }
}
