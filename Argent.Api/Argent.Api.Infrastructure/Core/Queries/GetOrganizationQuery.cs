using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Queries {
    /// <summary>
    /// Returns the organization with all its branches.
    /// </summary>
    /// <remarks>
    /// Since there is typically one organization per deployment,
    /// this query accepts an optional Id, if omitted, returns the first active org.
    /// </remarks>
    public record GetOrganizationQuery(long? OrganizationId = null)
        : IRequest<Result<OrganizationDto>>;

}
