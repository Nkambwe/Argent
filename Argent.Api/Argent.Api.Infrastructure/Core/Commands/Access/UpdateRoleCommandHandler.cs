using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class UpdateRoleCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<UpdateRoleCommand, Result<RoleDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<RoleDto>> Handle(
            UpdateRoleCommand command, CancellationToken ct) {
            var role = await _uow.Roles.GetWithPermissionsAsync(command.RoleId, ct);
            if (role is null)
                return Result<RoleDto>.NotFound("Role not found.");

            if (role.IsSystemRole)
                return Result<RoleDto>.Failure(
                    "System roles cannot be modified.", "SYSTEM_ROLE");

            if (role.Name != command.Name &&
                await _uow.Roles.NameExistsAsync(command.Name, command.RoleId, ct))
                return Result<RoleDto>.Failure($"A role named '{command.Name}' already exists.", "DUPLICATE_NAME");

            role.Name = command.Name;
            role.Description = command.Description;
            role.UpdatedBy = _userContext.Username;

            _uow.Roles.Update(role);
            await _uow.CommitAsync(ct);

            var updated = await _uow.Roles.GetWithPermissionsAsync(role.Id, ct);
            return Result<RoleDto>.Success(AccessMapper.MapToDto(updated!));
        }
    }

}
