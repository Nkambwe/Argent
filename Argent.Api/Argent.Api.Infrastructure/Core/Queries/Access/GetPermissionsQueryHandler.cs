using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public class GetPermissionsQueryHandler(IUnitOfWork uow)
                : IRequestHandler<GetPermissionsQuery, Result<IEnumerable<PermissionDto>>> {
        private readonly IUnitOfWork _uow = uow;

        public async Task<Result<IEnumerable<PermissionDto>>> Handle(GetPermissionsQuery query, CancellationToken ct) {
            var permissions = string.IsNullOrWhiteSpace(query.Module)
                ? await _uow.Permissions.GetAllAsync(ct)
                : await _uow.Permissions.GetByModuleAsync(query.Module, ct);

            var dtos = permissions.Select(p => new PermissionDto
            {
                Id = p.Id,
                Name = p.Name,
                Module = p.Module,
                Action = p.Action,
                Description = p.Description
            });
            return Result<IEnumerable<PermissionDto>>.Success(dtos);
        }
    }
}
