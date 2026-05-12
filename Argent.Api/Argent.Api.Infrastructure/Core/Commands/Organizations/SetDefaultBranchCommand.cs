using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    /// <summary>
    /// Command sets default branch for organization
    /// </summary>
    /// <param name="OrganizationId">Organization ID branch relates to</param>
    /// <param name="BranchId">Branch Id to set as default</param>
    public record SetDefaultBranchCommand(long BranchId)
    : IRequest<Result<BranchDto>>, IAuditableCommand {
        public string AuditModule => "Organization";
        public string AuditAction => "SetDefaultBranch";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "Branch";
    }

}
