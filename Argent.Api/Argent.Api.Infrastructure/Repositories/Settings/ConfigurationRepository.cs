using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Settings {
    public class ConfigurationRepository(AppDataContext context) : IConfigurationRepository {
        private readonly AppDataContext _context = context;

        public async Task<SystemConfiguration?> GetAsync(string module, string key, CancellationToken ct = default)
            => await _context.SystemConfigs
                .FirstOrDefaultAsync(c => c.Module == module && c.Key == key && !c.IsDeleted, ct);

        public async Task<IEnumerable<SystemConfiguration>> GetByModuleAsync(string module, CancellationToken ct = default)
            => await _context.SystemConfigs
                .Where(c => c.Module == module && !c.IsDeleted)
                .OrderBy(c => c.Key)
                .ToListAsync(ct);

        public async Task<IEnumerable<SystemConfiguration>> GetAllAsync(CancellationToken ct = default)
            => await _context.SystemConfigs
                .Where(c => !c.IsDeleted)
                .OrderBy(c => c.Module).ThenBy(c => c.Key)
                .ToListAsync(ct);

        public async Task UpsertAsync(string module, string key, string value,ConfigDataType dataType, CancellationToken ct = default) {
            var existing = await GetAsync(module, key, ct);
            if (existing is null) {
                await _context.SystemConfigs.AddAsync(new SystemConfiguration
                {
                    Module = module,
                    Key = key,
                    Value = value,
                    DataType = dataType
                }, ct);
            }
            else {
                existing.Value = value;
                existing.UpdatedOn = DateTime.UtcNow;
                _context.SystemConfigs.Update(existing);
            }
        }

        public async Task<IEnumerable<SystemPolicy>> GetPoliciesAsync(string? module = null, CancellationToken ct = default) {
            var query = _context.SystemPolicies.Where(p => !p.IsDeleted);
            if (module is not null)
                query = query.Where(p => p.Module == module);
            return await query.OrderBy(p => p.Module).ThenBy(p => p.Name).ToListAsync(ct);
        }

        public async Task<string?> GetEffectivePolicyValueAsync(string policyName, long roleGroupId, CancellationToken ct = default) {
            //..check for a role-group-level override first
            var overrideValue = await _context.RoleGroupPolicyOverrides
                .Where(o => o.RoleGroupId == roleGroupId
                         && o.SystemPolicy.Name == policyName
                         && !o.IsDeleted)
                .Select(o => o.OverrideValue)
                .FirstOrDefaultAsync(ct);

            if (overrideValue is not null)
                return overrideValue;

            //..fall back to system policy default
            return await _context.SystemPolicies
                .Where(p => p.Name == policyName && !p.IsDeleted)
                .Select(p => p.DefaultValue)
                .FirstOrDefaultAsync(ct);
        }
    }
}
