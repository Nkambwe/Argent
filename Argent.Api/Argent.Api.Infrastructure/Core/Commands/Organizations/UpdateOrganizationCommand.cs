using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    /// <summary>
    /// Command Update organization
    /// </summary>
    /// <param name="OrganizationId">Organization ID to update</param>
    public record UpdateOrganizationCommand(long OrganizationId, UpdateOrganizationRequest Request)
        : IRequest<Result<OrganizationDto>>, IAuditableCommand {
        public string AuditModule => "Organization";
        public string AuditAction => "UpdateOrganization";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "Organization";
    }
}
