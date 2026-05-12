using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public class GetRolesWithPermissionsQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetRolesWithPermissionsQuery, Result<IEnumerable<RoleDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<RoleDto>>> Handle(GetRolesWithPermissionsQuery query, CancellationToken token) {
            var roles = await _uow.Roles.GetAllAsync(token);
            var dtos = new List<RoleDto>();

            foreach (var role in roles) {
                var full = await _uow.Roles.GetByIdAsync(role.Id, token);
                dtos.Add(new RoleDto
                {
                    Id = role.Id,
                    Name = role.Name,
                    Description = role.Description,
                    IsSystemRole = role.IsSystemRole,
                    Permissions = [.. full!.RolePermissions.Where(rp => !rp.IsDeleted).Select(rp => new PermissionDto
                    {
                        Id = rp.Permission.Id,
                        Name = rp.Permission.Name,
                        Module = rp.Permission.Module,
                        Action = rp.Permission.Action,
                        Description = rp.Permission.Description
                    })]
                });
            }

            return Result<IEnumerable<RoleDto>>.Success(dtos);
        }
    }

}
