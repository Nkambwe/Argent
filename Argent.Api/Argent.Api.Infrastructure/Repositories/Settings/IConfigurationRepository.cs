using Argent.Api.Domain.Entities.Settings;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Repositories.Settings {
    public interface IConfigurationRepository {
        Task<SystemConfiguration?> GetAsync(string module, string key, CancellationToken token = default);
        Task<IEnumerable<SystemConfiguration>> GetByModuleAsync(string module, CancellationToken token = default);
        Task<IEnumerable<SystemConfiguration>> GetAllAsync(CancellationToken token = default);
        Task UpsertAsync(string module, string key, string value, ConfigDataType dataType, CancellationToken token = default);

        Task<IEnumerable<SystemPolicy>> GetPoliciesAsync(string? module = null, CancellationToken token = default);

        /// <summary>
        /// Resolves the effective value of a policy for a given role group,
        /// applying any override if one exists.
        /// Returns SystemPolicy.DefaultValue if no override exists.
        /// </summary>
        Task<string?> GetEffectivePolicyValueAsync(string policyName, long roleGroupId, CancellationToken token = default);
    }
}
