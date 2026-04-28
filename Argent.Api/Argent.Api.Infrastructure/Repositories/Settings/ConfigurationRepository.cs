using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Repositories.Settings {
    public class ConfigurationRepository(AppDataContext context) 
        : Repository<SystemConfiguration>(context), IConfigurationRepository {

        public async Task<SystemConfiguration?> GetAsync(string module, string key, CancellationToken ct = default)
            => await _context.SystemConfigs
                .FirstOrDefaultAsync(c => c.Module == module && c.Key == key && !c.IsDeleted, ct);

        public async Task<IEnumerable<SystemConfiguration>> GetByModuleAsync(string module, CancellationToken ct = default)
            => await _context.SystemConfigs
                .Where(c => c.Module == module && !c.IsDeleted)
                .OrderBy(c => c.Key)
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

    }
}
