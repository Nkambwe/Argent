using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Core.Queries;
using Argent.Api.Infrastructure.Logging;
using Argent.Api.Infrastructure.Transactions;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public class GetRoleGroupsQueryHandler(IUnitOfWork uow, IServiceLoggerFactory loggerFactory)
        : IRequestHandler<GetRoleGroupsQuery, Result<IEnumerable<RoleGroupDto>>> {
        private readonly IUnitOfWork _uow = uow;
        private readonly IServiceLoggerFactory _loggerFactory = loggerFactory;

        public async Task<Result<IEnumerable<RoleGroupDto>>> Handle(GetRoleGroupsQuery query, CancellationToken ct) {

            var logger = _loggerFactory.CreateLogger("access");
            logger.Channel = $"RETRIEVE-ROLEGROUP";
            logger.Log($"Retrieve all role groups", "INFO");

            var groups = await _uow.RoleGroups.Query()
                    .Include(g => g.Members.Where(m => !m.IsDeleted))
                    .ThenInclude(m => m.Role)
                    .Include(g => g.PolicyOverrides.Where(o => !o.IsDeleted))
                    .ThenInclude(o => o.SystemPolicy)
                .Where(g => !g.IsDeleted)
                .OrderBy(g => g.Name)
                .ToListAsync(ct);

            var dtos = groups.Select(g => new RoleGroupDto
            {
                Id = g.Id,
                Name = g.Name,
                Description = g.Description,
                IsActive = g.IsActive,
                CreatedOn = g.CreatedOn,
                Roles = g.Members.Select(m => new RoleGroupMemberDto
                {
                    RoleId = m.RoleId,
                    RoleName = m.Role?.Name ?? string.Empty
                }),
                PolicyOverrides = g.PolicyOverrides.Select(o => new PolicyOverrideDto
                {
                    Id = o.Id,
                    SystemPolicyId = o.SystemPolicyId,
                    PolicyName = o.SystemPolicy?.Name ?? string.Empty,
                    PolicyDescription = o.SystemPolicy?.Description ?? string.Empty,
                    OverrideValue = o.OverrideValue,
                    Reason = o.Reason
                })
            });

            return Result<IEnumerable<RoleGroupDto>>.Success(dtos);
        }
    }

}
