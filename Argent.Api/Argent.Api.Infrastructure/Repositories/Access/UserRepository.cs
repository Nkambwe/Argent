using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Access {
    public class UserRepository(AppDataContext context)
        : Repository<AppUser>(context), IUserRepository {
        public async Task<AppUser?> GetByUsernameAsync(string username, CancellationToken token = default)
            => await _dbSet.Include(u => u.DefaultBranch).FirstOrDefaultAsync(u => u.Username == username && !u.IsDeleted, token);

        public async Task<AppUser?> GetByEmailAsync(string email, CancellationToken token = default)
            => await _dbSet.Include(u => u.DefaultBranch)
                           .FirstOrDefaultAsync(u => u.Email == email && !u.IsDeleted, token);

        public async Task<AppUser?> GetWithAccessAsync(long userId, CancellationToken token = default)
            => await _dbSet.Include(u => u.DefaultBranch)
                .Include(u => u.UserRoles.Where(ur => !ur.IsDeleted))
                .ThenInclude(ur => ur.Role)
                .ThenInclude(r => r.RolePermissions.Where(rp => !rp.IsDeleted))
                .ThenInclude(rp => rp.Permission)
                .Include(u => u.BranchAccess.Where(ba => !ba.IsDeleted))
                .ThenInclude(ba => ba.Branch)
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, token);

        public async Task<IEnumerable<AppUser>> GetAllWithBranchAsync(CancellationToken token = default)
            => await _dbSet.Include(u => u.DefaultBranch)
                .Where(u => !u.IsDeleted)
                .OrderBy(u => u.LastName).ThenBy(u => u.FirstName)
                .ToListAsync(token);

        public async Task<bool> UsernameExistsAsync(string username, long? excludeId = null, CancellationToken ct = default)
            => await _dbSet.AnyAsync(u => u.Username == username && !u.IsDeleted &&
                (excludeId == null || u.Id != excludeId.Value), ct);

        public async Task<bool> EmailExistsAsync(string email, long? excludeId = null, CancellationToken ct = default)
            => await _dbSet.AnyAsync(u => u.Email == email && !u.IsDeleted &&
                (excludeId == null || u.Id != excludeId.Value), ct);

        #region Role assignment

        public async Task AddUserRoleAsync(UserRole userRole, CancellationToken token = default)
            => await _context.UserRoles.AddAsync(userRole, token);

        public async Task AssignRoleToUserAsync(long userId, long roleId, CancellationToken token = default) {
            var alreadyAssigned = await _context.UserRoles.AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId && !ur.IsDeleted, token);

            if (!alreadyAssigned)
                await _context.UserRoles.AddAsync(new UserRole { UserId = userId, RoleId = roleId }, token);
        }
        public async Task<UserRole?> GetUserRoleAsync(long userId, long roleId, CancellationToken ct = default)
            => await _context.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId, ct);

        public void RemoveUserRole(UserRole userRole) {
            userRole.IsDeleted = true;
            userRole.DeletedOn = DateTime.UtcNow;
        }

        public async Task<int> CountActiveRoleAssignmentsAsync(long roleId, CancellationToken ct)
            => await _context.UserRoles.CountAsync(ur => ur.RoleId == roleId, ct);

        #endregion

        #region Branch access

        public async Task<IEnumerable<UserBranchAccess>> GetBranchAccessAsync(long userId, CancellationToken token = default)
            => await _context.UserBranchAccess.Include(ba => ba.Branch)
                .Where(ba => ba.UserId == userId && !ba.IsDeleted)
                .ToListAsync(token);

        public async Task AddBranchAccessAsync(UserBranchAccess access, CancellationToken token = default)
            => await _context.UserBranchAccess.AddAsync(access, token);

        public async Task AssignBranchAccessAsync(long userId, long branchId, bool canPost, CancellationToken ct = default) {
            var existing = await _context.UserBranchAccess.FirstOrDefaultAsync(ba => ba.UserId == userId && ba.BranchId == branchId, ct);

            if (existing is null) {
                await _context.UserBranchAccess.AddAsync(new UserBranchAccess
                {
                    UserId = userId,
                    BranchId = branchId,
                    CanPost = canPost
                }, ct);
            }
            else if (existing.IsDeleted) {
                // Reactivate a previously removed access record
                existing.IsDeleted = false;
                existing.CanPost = canPost;
                existing.UpdatedOn = DateTime.UtcNow;
            }
        }

        public async Task<UserBranchAccess?> GetBranchAccessEntryAsync(long userId, long branchId, CancellationToken ct = default)
            => await _context.UserBranchAccess.FirstOrDefaultAsync(ba => ba.UserId == userId && ba.BranchId == branchId, ct);
        
        public async Task<AppUser?> GetByIdWithAccessAsync(long userId, CancellationToken ct = default)
            => await _context.Users
                .Include(u => u.DefaultBranch)
                .Include(u => u.UserRoles.Where(ur => !ur.IsDeleted))
                    .ThenInclude(ur => ur.Role)
                        .ThenInclude(r => r.RolePermissions.Where(rp => !rp.IsDeleted))
                            .ThenInclude(rp => rp.Permission)
                .Include(u => u.BranchAccess.Where(ba => !ba.IsDeleted))
                    .ThenInclude(ba => ba.Branch)
                .FirstOrDefaultAsync(u => u.Id == userId && !u.IsDeleted, ct);

        public void UpdateBranchAccess(UserBranchAccess access) {
            access.UpdatedOn = DateTime.UtcNow;
            _context.UserBranchAccess.Update(access);
        }

        #endregion

        #region Refresh tokens

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token, CancellationToken ct = default)
            => await _context.RefreshTokens.Include(rt => rt.User).FirstOrDefaultAsync(rt => rt.Token == token, ct);

        public async Task AddRefreshTokenAsync(RefreshToken token, CancellationToken ct = default)
            => await _context.RefreshTokens.AddAsync(token, ct);

        public void UpdateRefreshToken(RefreshToken token) {
            token.UpdatedOn = DateTime.UtcNow;
            _context.RefreshTokens.Update(token);
        }
        #endregion

        #region Password history

        public async Task<IEnumerable<PasswordHistory>> GetPasswordHistoryAsync(long userId, int count, CancellationToken token = default)
            => await _context.PasswordHistories.Where(ph => ph.UserId == userId && !ph.IsDeleted)
                .OrderByDescending(ph => ph.ChangedOn).Take(count).ToListAsync(token);

        public async Task AddPasswordHistoryAsync(PasswordHistory history, CancellationToken token = default)
            => await _context.PasswordHistories.AddAsync(history, token);

        #endregion
    }
}
