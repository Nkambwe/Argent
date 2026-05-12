using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public record GetRolesWithPermissionsQuery : IRequest<Result<IEnumerable<RoleDto>>>;
}
