using Argent.Api.Domain.Entities.Access;
using Argent.Api.Domain.Entities.Settings;

namespace Argent.Api.Infrastructure.Repositories.Access {

    /// <summary>
    /// Repository for the RoleGroup aggregate.
    /// </summary>
    /// <remarks>
    /// RoleGroup owns its members and policy overrides, both are managed here
    /// since they are not independent aggregates.
    /// </remarks>
    public interface IRoleGroupRepository : IRepository<RoleGroup> {
        Task<RoleGroup?> GetWithDetailsAsync(long id, CancellationToken token = default);
        Task<IEnumerable<RoleGroup>> GetAllWithDetailsAsync(CancellationToken ctokent = default);
        Task<bool> NameExistsAsync(string name, long? excludeId = null, CancellationToken token = default);
        Task AddMemberAsync(RoleGroupMember member, CancellationToken token = default);
        Task<RoleGroupMember?> GetMemberAsync(long roleGroupId, long roleId, CancellationToken token = default);
        Task<bool> MemberExistsAsync(long roleGroupId, long roleId, CancellationToken token = default);
        void RemoveMember(RoleGroupMember member);
        Task<RoleGroupPolicyOverride?> GetPolicyOverrideAsync(long roleGroupId, long systemPolicyId, CancellationToken token = default);
        Task AddPolicyOverrideAsync(RoleGroupPolicyOverride policyOverride, CancellationToken token = default);
        void UpdatePolicyOverride(RoleGroupPolicyOverride policyOverride);
        void RemovePolicyOverride(RoleGroupPolicyOverride policyOverride);
        /// <summary>
        /// Resolves the effective policy value for a user based on their role group memberships.
        /// Resolution order: RoleGroupOverride → SystemPolicy default.
        /// Returns null if no policy exists by that name.
        /// </summary>
        Task<string?> GetEffectivePolicyValueAsync(long userId, string policyName, CancellationToken token = default);

        /// <summary>
        /// Returns the first RoleGroup a user belongs to (via their roles).
        /// Used by BranchPolicyBehavior to resolve holiday/weekend overrides.
        /// </summary>
        Task<long?> GetUserRoleGroupIdAsync(long userId, CancellationToken token = default);
    }
}
