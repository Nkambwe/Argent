using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using Argent.Api.Infrastructure.Transactions;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public record GetPermissionsQuery(string? Module = null)
    : IRequest<Result<IEnumerable<PermissionDto>>>;
}
