using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organization {

    /// <summary>
    /// Command to create new oeganization
    /// </summary>
    /// <param name="RegisteredName">Organization registered name</param>
    /// <param name="ShortName">Organization Alias</param>
    /// <param name="RegistrationNumber">Company Registeration number</param>
    /// <param name="BusinessLine">Contact business line</param>
    /// <param name="ContactEmail">Contact email address</param>
    /// <param name="DefaultBranchCode">Default branch name</param>
    /// <param name="DefaultBranchAddress">Default branch address</param>
    /// <param name="DefaultBranchEmail">Default branch email address</param>
    /// <param name="DefaultBranchPostalAddress">Default branch postal address</param>
    public record CreateOrganizationCommand(
        string RegisteredName,
        string ShortName,
        string RegistrationNumber,
        string BusinessLine,
        string ContactEmail,
        string DefaultBranchCode,
        string DefaultBranchName,
        string DefaultBranchAddress,
        string DefaultBranchEmail,
        string? DefaultBranchPostalAddress
    ) : IRequest<Result<OrganizationDto>>;
}
