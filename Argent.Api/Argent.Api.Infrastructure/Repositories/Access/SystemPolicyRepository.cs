using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Access {
    public class SystemPolicyRepository(AppDataContext context) : 
        Repository<SystemPolicy>(context), ISystemPolicyRepository {

        public async Task<SystemPolicy?> GetPolicyByIdAsync(long id, CancellationToken ct = default)
            => await _context.SystemPolicies.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, ct);

        public async Task<SystemPolicy?> GetPolicyByNameAsync(
            string name, CancellationToken ct = default)
            => await _context.SystemPolicies
                .FirstOrDefaultAsync(p => p.Name == name && !p.IsDeleted, ct);

        public async Task<IEnumerable<SystemPolicy>> GetPoliciesAsync(string? module = null, CancellationToken ct = default) {
            var query = _context.SystemPolicies.Where(p => !p.IsDeleted);
            if (module is not null)
                query = query.Where(p => p.Module == module);
            return await query.OrderBy(p => p.Module).ThenBy(p => p.Name).ToListAsync(ct);
        }

        public async Task<string?> GetEffectivePolicyValueAsync(string policyName, long roleGroupId, CancellationToken ct = default) {
            // 1. Check for a group-level override
            var overrideValue = await _context.RoleGroupPolicyOverrides
                .Where(o => o.RoleGroupId == roleGroupId
                         && o.SystemPolicy.Name == policyName
                         && !o.IsDeleted)
                .Select(o => o.OverrideValue)
                .FirstOrDefaultAsync(ct);

            if (overrideValue is not null)
                return overrideValue;

            // 2. Fall back to system default
            return await _context.SystemPolicies
                .Where(p => p.Name == policyName && !p.IsDeleted)
                .Select(p => p.DefaultValue)
                .FirstOrDefaultAsync(ct);
        }
    }
}
