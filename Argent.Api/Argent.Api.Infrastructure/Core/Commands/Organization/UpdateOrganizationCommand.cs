using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organization {
    /// <summary>
    /// Command Update organization
    /// </summary>
    /// <param name="OrganizationId">Organization ID to update</param>
    /// <param name="RegisteredName">Registered organizational name</param>
    /// <param name="ShortName">Organizational Alias</param>
    /// <param name="BusinessLine">Contact business line</param>
    /// <param name="ContactEmail">Contact email address</param>
    public record UpdateOrganizationCommand(
        long OrganizationId,
        string RegisteredName,
        string ShortName,
        string BusinessLine,
        string ContactEmail
    ) : IRequest<Result<OrganizationDto>>;
}
