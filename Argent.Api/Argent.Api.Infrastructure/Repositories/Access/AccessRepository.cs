using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Access {
    public class AccessRepository(AppDataContext context) : IAccessRepository {
        private readonly AppDataContext _context = context;

        public async Task<AppUser?> GetUserByUsernameAsync(string username, CancellationToken token = default)
            => await _context.Users.Include(u => u.DefaultBranch)
                .FirstOrDefaultAsync(u => u.Username == username && !u.IsDeleted && u.IsActive, token);

        public async Task<AppUser?> GetUserByIdAsync(long userId, CancellationToken token = default)
            => await _context.Users.FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, token);

        public async Task<AppUser?> GetUserWithAccessAsync(long userId, CancellationToken token = default)
            => await _context.Users
                .Include(u => u.DefaultBranch)
                .Include(u => u.UserRoles)
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions)
                            .ThenInclude(rp => rp.Permission)
                .Include(u => u.BranchAccess)
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, token);

        public async Task<bool> UsernameExistsAsync(string username, CancellationToken token = default)
            => await _context.Users.AnyAsync(u => u.Username == username && !u.IsDeleted, token);

        public async Task<bool> EmailExistsAsync(string email, CancellationToken token = default)
            => await _context.Users.AnyAsync(u => u.Email == email && !u.IsDeleted, token);

        public async Task AddUserAsync(AppUser user, CancellationToken token = default)
            => await _context.Users.AddAsync(user, token);

        public void UpdateUser(AppUser user) {
            user.UpdatedOn = DateTime.UtcNow;
            _context.Users.Update(user);
        }

        public async Task<IEnumerable<Role>> GetAllRolesAsync(CancellationToken token = default)
            => await _context.Roles.Where(r => !r.IsDeleted).OrderBy(r => r.Name).ToListAsync(token);

        public async Task<Role?> GetRoleByNameAsync(string name, CancellationToken ct = default)
            => await _context.Roles.FirstOrDefaultAsync(r => r.Name == name && !r.IsDeleted, ct);

        public async Task AddRoleAsync(Role role, CancellationToken ct = default)
            => await _context.Roles.AddAsync(role, ct);

        public async Task<IEnumerable<Permission>> GetPermissionsByUserAsync(long userId, CancellationToken token = default)
            => await _context.UserRoles.Where(ur => ur.UserId == userId && !ur.IsDeleted)
                    .SelectMany(ur => ur.Role.RolePermissions.Where(rp => !rp.IsDeleted).Select(rp => rp.Permission))
                    .Distinct()
                    .ToListAsync(token);

        public async Task<Permission?> GetPermissionByNameAsync(string name, CancellationToken token = default)
            => await _context.Permissions.FirstOrDefaultAsync(p => p.Name == name && !p.IsDeleted, token);

        public async Task AddPermissionAsync(Permission permission, CancellationToken token = default)
            => await _context.Permissions.AddAsync(permission, token);

        public async Task SeedPermissionsAsync(IEnumerable<Permission> permissions, CancellationToken token = default) {
            foreach (var permission in permissions) {
                var exists = await _context.Permissions.AnyAsync(p => p.Name == permission.Name, token);
                if (!exists)
                    await _context.Permissions.AddAsync(permission, token);
            }
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct = default)
            => await _context.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == token && !rt.IsDeleted, ct);

        public async Task AddRefreshTokenAsync(RefreshToken token, CancellationToken ct = default)
            => await _context.RefreshTokens.AddAsync(token, ct);

        public void UpdateRefreshToken(RefreshToken token)
            => _context.RefreshTokens.Update(token);

        public async Task AssignRoleToUserAsync(long userId, long roleId, CancellationToken token = default) {
            var exists = await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId, token);
            if (!exists)
                await _context.UserRoles.AddAsync(new UserRole { UserId = userId, RoleId = roleId }, token);
        }

        public async Task AssignPermissionToRoleAsync(long roleId, long permissionId, CancellationToken token = default) {
            var exists = await _context.RolePermissions
                .AnyAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId, token);
            if (!exists)
                await _context.RolePermissions.AddAsync(
                    new RolePermission { RoleId = roleId, PermissionId = permissionId }, token);
        }

        public async Task AssignBranchAccessAsync(long userId, long branchId, bool canPost, CancellationToken ct = default) {
            var existing = await _context.UserBranchAccess
                .FirstOrDefaultAsync(ub => ub.UserId == userId && ub.BranchId == branchId, ct);
            if (existing is null)
                await _context.UserBranchAccess.AddAsync(
                    new UserBranchAccess { UserId = userId, BranchId = branchId, CanPost = canPost }, ct);
            else {
                existing.CanPost = canPost;
                existing.UpdatedOn = DateTime.UtcNow;
            }
        }
    }

}
