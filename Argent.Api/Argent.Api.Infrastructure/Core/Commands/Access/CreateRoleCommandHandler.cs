using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class CreateRoleCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<CreateRoleCommand, Result<RoleDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<RoleDto>> Handle(CreateRoleCommand command, CancellationToken ct) {
            if (await _uow.Roles.NameExistsAsync(command.Name, token: ct))
                return Result<RoleDto>.Failure($"A role named '{command.Name}' already exists.", "DUPLICATE_NAME");

            //..validate permissions
            foreach (var pid in command.PermissionIds) {
                var perm = await _uow.Permissions.GetByIdAsync(pid, ct);
                if (perm is null)
                    return Result<RoleDto>.NotFound($"Permission {pid} not found.");
            }

            return await _uow.ExecuteInTransactionAsync(async token => {
                var role = new Role {
                    Name = command.Name,
                    Description = command.Description,
                    IsSystemRole = false,
                    CreatedBy = _userContext.Username
                };

                await _uow.Roles.AddAsync(role, token);
                await _uow.CommitAsync(token);

                foreach (var pid in command.PermissionIds)
                    await _uow.Roles.AddPermissionAsync(new RolePermission {
                        RoleId = role.Id,
                        PermissionId = pid,
                        CreatedBy = _userContext.Username
                    }, token);

                var created = await _uow.Roles.GetWithPermissionsAsync(role.Id, token);
                return Result<RoleDto>.Success(AccessMapper.MapToDto(created!));
            }, ct);
        }
    }

}
