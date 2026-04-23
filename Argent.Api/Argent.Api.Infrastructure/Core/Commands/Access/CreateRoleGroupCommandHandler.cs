using Argent.Api.Domain.Entities.Access;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class CreateRoleGroupCommandHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory, IUserContext userContext)
                : IRequestHandler<CreateRoleGroupCommand, Result<RoleGroupDto>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;
        private readonly IUserContext _userContext = userContext;

        public async Task<Result<RoleGroupDto>> Handle(CreateRoleGroupCommand command, CancellationToken token) {

            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = $"CREATE-ROLE-GROUP-{command.Name}";
            logger.Log($"Creating role group: {command.Name}", "INFO");

            //..make sure we have a unique name
            if (await _uow.RoleGroups.ExistsAsync(g => g.Name == command.Name && !g.IsDeleted, token))
                return Result<RoleGroupDto>.Failure($"A role group named '{command.Name}' already exists.", "DUPLICATE_NAME");

            //..validate all supplied roles exist
            var roles = new List<Role>();
            foreach (var roleId in command.RoleIds) {
                var role = await _uow.Roles.GetByIdAsync(roleId, token);
                if (role is null) {
                    logger.Log($"Not Found!: Selected role not found", "INFO");
                    return Result<RoleGroupDto>.NotFound($"Role {roleId} not found.");
                }
                    
                roles.Add(role);
            }

            var result = await _uow.ExecuteInTransactionAsync(async token => {
                //..create role
                var group = new RoleGroup {
                    Name = command.Name,
                    Description = command.Description,
                    IsActive = true
                };

                await _uow.RoleGroups.AddAsync(group, token);
                //..first SaveChanges to get Group.Id
                await _uow.CommitAuditAsync(token);

                foreach (var roleId in command.RoleIds)
                    await _uow.RoleGroupMembers.AddAsync(new RoleGroupMember {
                        RoleGroupId = group.Id,
                        RoleId = roleId,
                        CreatedBy = _userContext.Username
                    }, token);

                //..second SaveChanges for group members
                await _uow.CommitAuditAsync(token);

                //..log record
                logger.Log($"Role Group created: {group.Name} >> {group.Description}", "INFO");

                return new RoleGroupDto {
                    Id = group.Id,
                    Name = group.Name,
                    Description = group.Description,
                    IsActive = group.IsActive,
                    CreatedOn = group.CreatedOn,
                    Roles = roles.Select(r => new RoleGroupMemberDto {
                        RoleId = r.Id,
                        RoleName = r.Name
                    })
                };

            }, token);

            return Result<RoleGroupDto>.Success(result);
            
        }
    }
}
