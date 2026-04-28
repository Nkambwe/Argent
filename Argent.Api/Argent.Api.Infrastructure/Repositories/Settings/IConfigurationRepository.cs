using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Repositories.Settings {
    public interface IConfigurationRepository: IRepository<SystemConfiguration> {
        Task<SystemConfiguration?> GetAsync(string module, string key, CancellationToken token = default);
        Task<IEnumerable<SystemConfiguration>> GetByModuleAsync(string module, CancellationToken token = default);
        Task UpsertAsync(string module, string key, string value, ConfigDataType dataType, CancellationToken token = default);
    }
}
