using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public class GetRoleByIdQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetRoleByIdQuery, Result<RoleDto>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<RoleDto>> Handle(GetRoleByIdQuery query, CancellationToken ct) {
            var role = await _uow.Roles.GetWithPermissionsAsync(query.Id, ct);
            if (role is null)
                return Result<RoleDto>.NotFound("Role not found.");

            return Result<RoleDto>.Success(new RoleDto
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description,
                IsSystemRole = role.IsSystemRole,
                Permissions = role.RolePermissions
                    .Where(rp => !rp.IsDeleted)
                    .Select(rp => new PermissionDto
                    {
                        Id = rp.Permission!.Id,
                        Name = rp.Permission.Name,
                        Module = rp.Permission.Module,
                        Action = rp.Permission.Action,
                        Description = rp.Permission.Description
                    })
            });
        }
    }
}
