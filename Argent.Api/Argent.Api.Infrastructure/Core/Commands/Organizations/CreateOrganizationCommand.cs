using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {

    /// <summary>
    /// Command to create new oeganization
    /// </summary>
    public record CreateOrganizationCommand(CreateOrganizationRequest Request)
    : IRequest<Result<OrganizationDto>>, IAuditableCommand {
        public string AuditModule => "Organization";
        public string AuditAction => "CreateOrganization";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "Organization";
    }

}
