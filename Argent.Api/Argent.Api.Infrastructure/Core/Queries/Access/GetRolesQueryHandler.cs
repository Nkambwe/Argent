using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public class GetRolesQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetRolesQuery, Result<IEnumerable<RoleSummaryDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<RoleSummaryDto>>> Handle(GetRolesQuery query, CancellationToken ct) {
            var roles = await _uow.Roles.GetAllWithPermissionsAsync(ct);
            var dtos = roles.Select(r => new RoleSummaryDto
            {
                Id = r.Id,
                Name = r.Name,
                Description = r.Description,
                IsSystemRole = r.IsSystemRole,
                PermissionCount = r.RolePermissions.Count(rp => !rp.IsDeleted)
            });
            return Result<IEnumerable<RoleSummaryDto>>.Success(dtos);
        }
    }

}
