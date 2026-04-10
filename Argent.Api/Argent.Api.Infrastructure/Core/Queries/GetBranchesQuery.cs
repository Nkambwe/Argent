using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Queries {
    /// <summary>
    /// Query to retrieve branches
    /// </summary>
    /// <param name="OrganizationId"></param>
    public record GetBranchesQuery(
        long OrganizationId
    ) : IRequest<Result<IEnumerable<BranchDto>>>;
}
