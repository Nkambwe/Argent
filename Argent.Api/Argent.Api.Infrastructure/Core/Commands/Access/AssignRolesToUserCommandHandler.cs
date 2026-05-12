using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class AssignRolesToUserCommandHandler(IUnitOfWork uow, IUserContext userContext)
                : IRequestHandler<AssignRolesToUserCommand, Result<UserDetailDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<UserDetailDto>> Handle(AssignRolesToUserCommand command, CancellationToken ct) {
            var user = await _uow.Users.GetWithAccessAsync(command.UserId, ct);
            if (user is null)
                return Result<UserDetailDto>.NotFound("User not found.");

            return await _uow.ExecuteInTransactionAsync(async token => {
                foreach (var roleId in command.RoleIds) {
                    var role = await _uow.Roles.GetByIdAsync(roleId, token);
                    if (role is null)
                        throw new KeyNotFoundException($"Role {roleId} not found.");

                    var existing = await _uow.Users.GetUserRoleAsync(command.UserId, roleId, token);
                    if (existing is null) {
                        await _uow.Users.AddUserRoleAsync(new UserRole
                        {
                            UserId = command.UserId,
                            RoleId = roleId,
                            CreatedBy = _userContext.Username
                        }, token);
                    }
                    else if (existing.IsDeleted) {
                        existing.IsDeleted = false;
                        existing.DeletedOn = null;
                        existing.UpdatedBy = _userContext.Username;
                    }
                }

                var updated = await _uow.Users.GetWithAccessAsync(command.UserId, token);
                return Result<UserDetailDto>.Success(AccessMapper.MapUserToDetailDto(updated!));
            }, ct);
        }
    }

}
