using Argent.Api.Domain.Entities.Access;

namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public static class RoleGroupMapper {
        public static RoleGroupDto MapToDto(RoleGroup g) => new()
        {
            Id = g.Id,
            Name = g.Name,
            Description = g.Description,
            IsActive = g.IsActive,
            CreatedOn = g.CreatedOn,
            Roles = g.Members
                .Where(m => !m.IsDeleted)
                .Select(m => new RoleGroupMemberDto
                {
                    RoleId = m.RoleId,
                    RoleName = m.Role?.Name ?? string.Empty
                }),
            PolicyOverrides = g.PolicyOverrides
                .Where(o => !o.IsDeleted)
                .Select(o => new PolicyOverrideDto
                {
                    Id = o.Id,
                    SystemPolicyId = o.SystemPolicyId,
                    PolicyName = o.SystemPolicy?.Name ?? string.Empty,
                    PolicyDescription = o.SystemPolicy?.Description ?? string.Empty,
                    OverrideValue = o.OverrideValue,
                    Reason = o.Reason
                })
        };
    }

}
