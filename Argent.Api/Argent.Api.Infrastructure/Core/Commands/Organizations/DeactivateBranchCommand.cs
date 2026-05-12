using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Organizations {
    public record DeactivateBranchCommand(long BranchId)
    : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Organization";
        public string AuditAction => "DeactivateBranch";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Delete;
        public string? AuditEntityName => "Branch";
    }
}
