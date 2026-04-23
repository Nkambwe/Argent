using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class UpdateRoleGroupCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory, IUserContext userContext) 
        : IRequestHandler<UpdateRoleGroupCommand, Result<RoleGroupDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<RoleGroupDto>> Handle(UpdateRoleGroupCommand command, CancellationToken token) {
            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = $"UPDATE-ROLEGROUP-{command.Name}";
            logger.Log($"Upating role group: {command.Name}", "INFO");

            //..get record
            var group = await _uow.RoleGroups.Query().Include(g => g.Members)
                                                     .ThenInclude(m => m.Role)
                                                     .Include(g => g.PolicyOverrides)
                                                     .ThenInclude(o => o.SystemPolicy)
                                                     .FirstOrDefaultAsync(g => g.Id == command.RoleGroupId, token);

            if (group is null) {
                logger.Log($"Not Found!: Role group not found", "INFO");
                return Result<RoleGroupDto>.NotFound("Role group not found.");
            }

            // Check name uniqueness if changing name
            if (group.Name != command.Name && await _uow.RoleGroups.ExistsAsync(g => g.Name == command.Name && !g.IsDeleted && g.Id != group.Id, token)) {
                logger.Log($"Duplicate Record!: A role group named '{command.Name}' already exists.", "INFO");
                return Result<RoleGroupDto>.Failure($"A role group named '{command.Name}' already exists.", "DUPLICATE_NAME");
            }
            
            group.Name = command.Name;
            group.Description = command.Description;
            group.IsActive = command.IsActive;
            group.UpdatedOn = DateTime.UtcNow;
            group.UpdatedBy = _userContext.Username;

            _uow.RoleGroups.Update(group);
            //..first SaveChanges to get user.Id
            await _uow.CommitAuditAsync(token);
            logger.Log($"Role Group updated: {group.Name} >>  {group.Description})", "INFO");
            return Result<RoleGroupDto>.Success(new RoleGroupDto {
                Id = group.Id,
                Name = group.Name,
                Description = group.Description,
                IsActive = group.IsActive,
                CreatedOn = group.CreatedOn,
                Roles = group.Members.Select(m => new RoleGroupMemberDto {
                    RoleId = m.RoleId,
                    RoleName = m.Role?.Name ?? string.Empty
                }),
                PolicyOverrides = group.PolicyOverrides.Select(o => new PolicyOverrideDto
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
