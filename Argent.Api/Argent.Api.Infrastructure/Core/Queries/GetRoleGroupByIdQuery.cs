using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    public record GetRoleGroupByIdQuery(long Id) : IRequest<Result<RoleGroupDto>>;

}
