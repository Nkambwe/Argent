using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    /// <summary>
    /// Query to retrieve branch by ID
    /// </summary>
    /// <param name="OrganizationId">Organization ID the branch belongs to</param>
    /// <param name="BranchId">Branch ID to look for</param>
    public record GetBranchByIdQuery(
        long OrganizationId, 
        long BranchId
    ) : IRequest<Result<BranchDto>>;
}
