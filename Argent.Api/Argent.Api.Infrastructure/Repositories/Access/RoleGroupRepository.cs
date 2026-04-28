using Argent.Api.Domain.Entities.Access;
using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Access {

    public class RoleGroupRepository(AppDataContext context) : Repository<RoleGroup>(context), IRoleGroupRepository {
        public async Task<RoleGroup?> GetWithDetailsAsync(long id, CancellationToken token = default)
            => await _dbSet.Include(g => g.Members.Where(m => !m.IsDeleted))
                           .ThenInclude(m => m.Role)
                           .Include(g => g.PolicyOverrides.Where(o => !o.IsDeleted))
                           .ThenInclude(o => o.SystemPolicy)
                           .FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted, token);

        public async Task<IEnumerable<RoleGroup>> GetAllWithDetailsAsync(CancellationToken token = default)
            => await _dbSet.Include(g => g.Members.Where(m => !m.IsDeleted))
                    .ThenInclude(m => m.Role)
                    .Include(g => g.PolicyOverrides.Where(o => !o.IsDeleted))
                    .ThenInclude(o => o.SystemPolicy)
                    .Where(g => !g.IsDeleted).OrderBy(g => g.Name).ToListAsync(token);

        public async Task<bool> NameExistsAsync(string name, long? excludeId = null, CancellationToken token = default)
            => await _dbSet.AnyAsync(g =>g.Name == name && !g.IsDeleted && (excludeId == null || g.Id != excludeId.Value), token);

        #region Member management

        public async Task AddMemberAsync(RoleGroupMember member, CancellationToken token = default)
            => await _context.RoleGroupMembers.AddAsync(member, token);

        public async Task<RoleGroupMember?> GetMemberAsync(long roleGroupId, long roleId, CancellationToken token = default)
            => await _context.RoleGroupMembers.FirstOrDefaultAsync(m => m.RoleGroupId == roleGroupId && m.RoleId == roleId, token);  

        public async Task<bool> MemberExistsAsync(long roleGroupId, long roleId, CancellationToken token = default)
            => await _context.RoleGroupMembers.AnyAsync(m => m.RoleGroupId == roleGroupId && m.RoleId == roleId && !m.IsDeleted, token);

        public void RemoveMember(RoleGroupMember member) {
            member.IsDeleted = true;
            member.DeletedOn = DateTime.UtcNow;
        }

        #endregion

        #region Policy override management

        public async Task<RoleGroupPolicyOverride?> GetPolicyOverrideAsync(long roleGroupId, long systemPolicyId, CancellationToken ct = default)
            => await _context.RoleGroupPolicyOverrides.FirstOrDefaultAsync(o => o.RoleGroupId == roleGroupId && o.SystemPolicyId == systemPolicyId, ct);

        public async Task AddPolicyOverrideAsync(RoleGroupPolicyOverride policyOverride, CancellationToken ct = default)
            => await _context.RoleGroupPolicyOverrides.AddAsync(policyOverride, ct);

        public void UpdatePolicyOverride(RoleGroupPolicyOverride policyOverride) {
            policyOverride.UpdatedOn = DateTime.UtcNow;
            _context.RoleGroupPolicyOverrides.Update(policyOverride);
        }

        public void RemovePolicyOverride(RoleGroupPolicyOverride policyOverride) {
            policyOverride.IsDeleted = true;
            policyOverride.DeletedOn = DateTime.UtcNow;
        }

        #endregion

        #region Policy resolution

        public async Task<string?> GetEffectivePolicyValueAsync(long userId, string policyName, CancellationToken ct = default) {

            var overrideValue = await _context.RoleGroupMembers
                .Where(m => !m.IsDeleted && m.Role.UserRoles.Any(ur => ur.UserId == userId && !ur.IsDeleted))
                .SelectMany(m => m.RoleGroup.PolicyOverrides.Where(o => !o.IsDeleted && o.SystemPolicy.Name == policyName))
                .Select(o => o.OverrideValue)
                .FirstOrDefaultAsync(ct);

            if (overrideValue is not null)
                return overrideValue;

            return await _context.SystemPolicies.Where(p => p.Name == policyName && !p.IsDeleted)
                .Select(p => p.DefaultValue)
                .FirstOrDefaultAsync(ct);
        }

        public async Task<long?> GetUserRoleGroupIdAsync(long userId, CancellationToken token = default)
            => await _context.RoleGroupMembers.Where(m => !m.IsDeleted && m.Role.UserRoles
                .Any(ur => ur.UserId == userId && !ur.IsDeleted))
                .Select(m => (long?)m.RoleGroupId)
                .FirstOrDefaultAsync(token);

        #endregion
    }

}
