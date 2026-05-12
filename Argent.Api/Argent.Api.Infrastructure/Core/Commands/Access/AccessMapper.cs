using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public static class AccessMapper {
        public static RoleDto MapToDto(Role r) => new()
        {
            Id = r.Id,
            Name = r.Name,
            Description = r.Description,
            IsSystemRole = r.IsSystemRole,
            Permissions = r.RolePermissions
                .Where(rp => !rp.IsDeleted)
                .Select(rp => new PermissionDto
                {
                    Id = rp.Permission!.Id,
                    Name = rp.Permission.Name,
                    Module = rp.Permission.Module,
                    Action = rp.Permission.Action,
                    Description = rp.Permission.Description
                })
        };

        public static UserDetailDto MapUserToDetailDto(AppUser u) => new()
        {
            Id = u.Id,
            FullName = $"{u.FirstName} {u.MiddleName} {u.LastName}".Replace("  ", " ").Trim(),
            MiddleName = u.MiddleName,
            Username = u.Username,
            Email = u.Email,
            PhoneNumber = u.PhoneNumber,
            IsActive = u.IsActive,
            LastLoginOn = u.LastLoginOn,
            DefaultBranchId = u.DefaultBranchId,
            DefaultBranchName = u.DefaultBranch?.BranchName ?? string.Empty,
            CreatedOn = u.CreatedOn,
            Roles = u.UserRoles
                .Where(ur => !ur.IsDeleted)
                .Select(ur => ur.Role?.Name ?? string.Empty),
            BranchAccess = u.BranchAccess
                .Where(ba => !ba.IsDeleted)
                .Select(ba => new BranchAccessDto
                {
                    BranchId = ba.BranchId,
                    BranchName = ba.Branch?.BranchName ?? string.Empty,
                    CanPost = ba.CanPost,
                    IsDefault = ba.BranchId == u.DefaultBranchId
                })
        };
    }

}
