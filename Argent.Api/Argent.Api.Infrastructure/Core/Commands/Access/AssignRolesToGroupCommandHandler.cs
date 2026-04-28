using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class AssignRolesToGroupCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory, IUserContext userContext)
       : IRequestHandler<AssignRolesToGroupCommand, Result<RoleGroupDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<RoleGroupDto>> Handle(AssignRolesToGroupCommand command, CancellationToken ct) {

            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = "ROLE-GROUPS";

            logger.Log($"Assign roles to role groups", "SECURITY-ALERT");
            var group = await _uow.RoleGroups.GetByIdAsync(command.RoleGroupId, ct);
            if (group is null)
                return Result<RoleGroupDto>.NotFound("Role group not found.");

            return await _uow.ExecuteInTransactionAsync(async token =>
            {
                foreach (var roleId in command.RoleIds) {
                    var role = await _uow.Roles.GetByIdAsync(roleId, token) 
                    ?? throw new KeyNotFoundException($"Role {roleId} not found.");
                    var existing = await _uow.RoleGroups.GetMemberAsync(
                        command.RoleGroupId, roleId, token);

                    if (existing is null) {
                        await _uow.RoleGroups.AddMemberAsync(new RoleGroupMember
                        {
                            RoleGroupId = command.RoleGroupId,
                            RoleId = roleId,
                            CreatedBy = _userContext.Username
                        }, token);
                    }
                    else if (existing.IsDeleted) {
                        // Reactivate soft-deleted membership
                        existing.IsDeleted = false;
                        existing.DeletedOn = null;
                        existing.UpdatedOn = DateTime.UtcNow;
                        existing.UpdatedBy = _userContext.Username;
                    }
                }

                var updated = await _uow.RoleGroups.GetWithDetailsAsync(command.RoleGroupId, token);
                return Result<RoleGroupDto>.Success(RoleGroupMapper.MapToDto(updated!));
            }, ct);
        }
    }

}
