using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Repositories.Settings;
using Microsoft.Extensions.Caching.Memory;

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

    public class SystemConfigurationService(IConfigurationRepository repo, IMemoryCache cache) : ISystemConfigurationService {
        private readonly IConfigurationRepository _repo = repo;
        private readonly IMemoryCache _cache = cache;
        private static readonly TimeSpan CacheTtl = TimeSpan.FromMinutes(5);

        public async Task<string> GetStringAsync(string module, string key, string defaultValue = "", CancellationToken ct = default) {
            var cacheKey = CacheKey(module, key);
            if (_cache.TryGetValue(cacheKey, out string? cached))
                return cached ?? defaultValue;

            var config = await _repo.GetAsync(module, key, ct);
            var value = config?.Value ?? defaultValue;
            _cache.Set(cacheKey, value, CacheTtl);
            return value;
        }

        public async Task<int> GetIntAsync(string module, string key, int defaultValue = 0, CancellationToken ct = default) {
            var raw = await GetStringAsync(module, key, defaultValue.ToString(), ct);
            return int.TryParse(raw, out var result) ? result : defaultValue;
        }

        public async Task<bool> GetBoolAsync(string module, string key, bool defaultValue = false, CancellationToken ct = default) {
            var raw = await GetStringAsync(module, key, defaultValue.ToString(), ct);
            return bool.TryParse(raw, out var result) ? result : defaultValue;
        }

        public async Task<decimal> GetDecimalAsync(string module, string key, decimal defaultValue = 0, CancellationToken ct = default) {
            var raw = await GetStringAsync(module, key, defaultValue.ToString(), ct);
            return decimal.TryParse(raw, out var result) ? result : defaultValue;
        }

        public async Task SetAsync(string module, string key, string value, ConfigDataType dataType, CancellationToken ct = default) {
            await _repo.UpsertAsync(module, key, value, dataType, ct);
            InvalidateCache(module, key);
        }

        public void InvalidateCache(string? module = null, string? key = null) {
            if (module is not null && key is not null)
                _cache.Remove(CacheKey(module, key));
            // For broader invalidation: restart app or use distributed cache with tags
        }

        public async Task<bool> GetPolicyBoolAsync(string policyName, long? roleGroupId = null, bool defaultValue = false, CancellationToken ct = default) {
            var raw = await GetPolicyStringAsync(policyName, roleGroupId, defaultValue.ToString(), ct);
            return bool.TryParse(raw, out var result) ? result : defaultValue;
        }

        public async Task<string> GetPolicyStringAsync(string policyName, long? roleGroupId = null, string defaultValue = "", CancellationToken token = default) {
            if (roleGroupId.HasValue) {
                //..always live for group-level overrides, verrides change less frequently but must be accurate
                var effective = await _repo.GetEffectivePolicyValueAsync(policyName, roleGroupId.Value, token);
                return effective ?? defaultValue;
            }

            //..no group context, return system default from cache
            var cacheKey = $"policy:{policyName}";
            if (_cache.TryGetValue(cacheKey, out string? cached))
                return cached ?? defaultValue;

            var policies = await _repo.GetPoliciesAsync(token: token);
            var policy = policies.FirstOrDefault(p => p.Name == policyName);
            var value = policy?.DefaultValue ?? defaultValue;
            _cache.Set(cacheKey, value, CacheTtl);
            return value;
        }

        private static string CacheKey(string module, string key) => $"sysconfig:{module}:{key}";
    }

}
