using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Argent.Api.Infrastructure.Core.Queries {
    public class GetRoleGroupByIdQueryHandler
    : IRequestHandler<GetRoleGroupByIdQuery, Result<RoleGroupDto>> {
        private readonly AppDataContext _db;
        public GetRoleGroupByIdQueryHandler(AppDataContext db) => _db = db;

        public async Task<Result<RoleGroupDto>> Handle(
            GetRoleGroupByIdQuery query, CancellationToken ct) {
            var g = await _db.RoleGroups
                .Include(rg => rg.Members.Where(m => !m.IsDeleted))
                    .ThenInclude(m => m.Role)
                .Include(rg => rg.PolicyOverrides.Where(o => !o.IsDeleted))
                    .ThenInclude(o => o.SystemPolicy)
                .FirstOrDefaultAsync(rg => rg.Id == query.Id && !rg.IsDeleted, ct);

            if (g is null)
                return Result<RoleGroupDto>.NotFound("Role group not found.");

            return Result<RoleGroupDto>.Success(new RoleGroupDto
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
        }
    }

}
