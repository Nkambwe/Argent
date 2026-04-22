using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Services {

    public interface ISystemConfigurationService {
        Task<string> GetStringAsync(string module, string key, string defaultValue = "", CancellationToken token = default);
        Task<int> GetIntAsync(string module, string key, int defaultValue = 0, CancellationToken token = default);
        Task<bool> GetBoolAsync(string module, string key, bool defaultValue = false, CancellationToken token = default);
        Task<decimal> GetDecimalAsync(string module, string key, decimal defaultValue = 0, CancellationToken token = default);
        Task SetAsync(string module, string key, string value, ConfigDataType dataType, CancellationToken token = default);
        void InvalidateCache(string? module = null, string? key = null);

        //..policy resolution
        Task<bool> GetPolicyBoolAsync(string policyName, long? roleGroupId = null, bool defaultValue = false, CancellationToken token = default);
        Task<string> GetPolicyStringAsync(string policyName, long? roleGroupId = null, string defaultValue = "", CancellationToken token = default);
    }

}
