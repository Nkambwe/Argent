using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Organization.DataObjects;
using Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    /// <summary>
    /// Command to create new Branch
    /// </summary>
    public record CreateBranchCommand(CreateBranchRequest Request)
        : IRequest<Result<BranchDto>>, IAuditableCommand {
        public string AuditModule => "Organization";
        public string AuditAction => "CreateBranch";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "Branch";
    }
}
