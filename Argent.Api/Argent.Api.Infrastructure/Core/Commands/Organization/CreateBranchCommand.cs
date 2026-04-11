using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organization {
    /// <summary>
    /// Command to create new Branch
    /// </summary>
    /// <param name="OrganizationId">Organization ID</param>
    /// <param name="BranchName">Branch Name</param>
    /// <param name="Address">Branch Physical address</param>
    /// <param name="EmailAddress">Branch Mail Address</param>
    /// <param name="PostalAddress">Branch Postal Address</param>
    /// <param name="MakeDefault">Mark as default branch</param>
    public record CreateBranchCommand(
        long OrganizationId,
        string BranchCode,
        string BranchName,
        string Address,
        string EmailAddress,
        string? PostalAddress,
        bool MakeDefault
    ) : IRequest<Result<BranchDto>>;
}
