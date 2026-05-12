using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class AssignPermissionsToRoleCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<AssignPermissionsToRoleCommand, Result<RoleDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<RoleDto>> Handle(
            AssignPermissionsToRoleCommand command, CancellationToken ct) {
            var role = await _uow.Roles.GetWithPermissionsAsync(command.RoleId, ct);
            if (role is null)
                return Result<RoleDto>.NotFound("Role not found.");

            var existing = role.RolePermissions
                .Where(rp => !rp.IsDeleted)
                .Select(rp => rp.PermissionId)
                .ToHashSet();

            return await _uow.ExecuteInTransactionAsync(async token => {
                foreach (var pid in command.PermissionIds) {
                    if (existing.Contains(pid)) continue;

                    var perm = await _uow.Permissions.GetByIdAsync(pid, token);
                    if (perm is null)
                        throw new KeyNotFoundException($"Permission {pid} not found.");

                    await _uow.Roles.AddPermissionAsync(new RolePermission {
                        RoleId = command.RoleId,
                        PermissionId = pid,
                        CreatedBy = _userContext.Username
                    }, token);
                }

                var updated = await _uow.Roles.GetWithPermissionsAsync(command.RoleId, token);
                return Result<RoleDto>.Success(AccessMapper.MapToDto(updated!));
            }, ct);
        }
    }

}
