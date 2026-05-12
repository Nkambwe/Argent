using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries.Access {
    public record GetUserByIdQuery(long Id)  
        : IRequest<Result<UserDetailDto>>;
}
