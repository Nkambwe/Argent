using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Access.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    public record GetSystemPoliciesQuery(string? Module = null)
        : IRequest<Result<IEnumerable<SystemPolicyDto>>>;
}
