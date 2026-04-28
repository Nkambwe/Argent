using Argent.Api.Domain.Entities.Settings;

namespace Argent.Api.Infrastructure.Repositories.Access {

    public interface ISystemPolicyRepository : IRepository<SystemPolicy> {
        Task<SystemPolicy?> GetPolicyByIdAsync(long id, CancellationToken ct = default);
        Task<IEnumerable<SystemPolicy>> GetPoliciesAsync(string? module = null, CancellationToken token = default);
        Task<string?> GetEffectivePolicyValueAsync(string policyName, long roleGroupId, CancellationToken token = default);
    }
}
