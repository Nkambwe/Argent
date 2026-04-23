using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class AssignRolesToGroupCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory, IUserContext userContext)
        : IRequestHandler<AssignRolesToGroupCommand, Result<RoleGroupDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<RoleGroupDto>> Handle(AssignRolesToGroupCommand command, CancellationToken token) {

            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = $"ASSIGNE-ROLESTOGROUP";
            logger.Log($"Assigne user roles to role group {command.RoleGroupId}", "INFO");

            var group = await _uow.RoleGroups.Query()
                    .Include(g => g.Members.Where(m => !m.IsDeleted))
                    .ThenInclude(m => m.Role)
                    .FirstOrDefaultAsync(g => g.Id == command.RoleGroupId && !g.IsDeleted, token);

            if (group is null) {
                logger.Log($"Not Found!: Role group not found", "INFO");
                return Result<RoleGroupDto>.NotFound("Role group not found.");
            }

            var existingRoleIds = group.Members.Select(m => m.RoleId).ToHashSet();

            foreach (var roleId in command.RoleIds) {
                if (existingRoleIds.Contains(roleId)) continue;

                var role = await _uow.Roles.GetFirstOrDefaultAsync(r => r.Id == roleId && !r.IsDeleted, token);
                if (role is null) {
                    logger.Log($"Not Found!: Role with ID {roleId} not found", "INFO");
                    return Result<RoleGroupDto>.NotFound($"Role {roleId} not found.");
                }

                await _uow.RoleGroupMembers.AddAsync(new RoleGroupMember {
                    RoleGroupId = group.Id,
                    RoleId = roleId,
                    CreatedBy = _userContext.Username
                }, token);
            }

            // ..second SaveChanges for group members
            await _uow.CommitAuditAsync(token);

            // Reload for fresh response
            var updated = await _uow.RoleGroups.Query()
                .Include(g => g.Members.Where(m => !m.IsDeleted)).ThenInclude(m => m.Role)
                .Include(g => g.PolicyOverrides.Where(o => !o.IsDeleted)).ThenInclude(o => o.SystemPolicy)
                .FirstAsync(g => g.Id == command.RoleGroupId, token);

            return Result<RoleGroupDto>.Success(new RoleGroupDto
            {
                Id = updated.Id,
                Name = updated.Name,
                Description = updated.Description,
                IsActive = updated.IsActive,
                CreatedOn = updated.CreatedOn,
                Roles = updated.Members.Select(m => new RoleGroupMemberDto
                {
                    RoleId = m.RoleId,
                    RoleName = m.Role?.Name ?? string.Empty
                }),
                PolicyOverrides = updated.PolicyOverrides.Select(o => new PolicyOverrideDto
                {
                    Id = o.Id,
                    SystemPolicyId = o.SystemPolicyId,
                    PolicyName = o.SystemPolicy?.Name ?? string.Empty,
                    PolicyDescription = o.SystemPolicy?.Description ?? string.Empty,
                    OverrideValue = o.OverrideValue,
                    Reason = o.Reason
                })
            });
        }
    }

}
