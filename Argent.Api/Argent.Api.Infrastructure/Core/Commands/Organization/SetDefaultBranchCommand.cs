using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organization {
    /// <summary>
    /// Command sets default branch for organization
    /// </summary>
    /// <param name="OrganizationId">Organization ID branch relates to</param>
    /// <param name="BranchId">Branch Id to set as default</param>
    public record SetDefaultBranchCommand(
        long OrganizationId,
        long BranchId
    ) : IRequest<Result<BranchDto>>;

}
