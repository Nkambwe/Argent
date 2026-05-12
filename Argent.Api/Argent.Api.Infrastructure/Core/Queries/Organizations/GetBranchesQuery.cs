using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using MediatR;


namespace Argent.Api.Infrastructure.Core.Queries.Organizations {
    /// <summary>
    /// Query to retrieve branches
    /// </summary>
    public record GetBranchesQuery() : IRequest<Result<IEnumerable<BranchDto>>>;
}
