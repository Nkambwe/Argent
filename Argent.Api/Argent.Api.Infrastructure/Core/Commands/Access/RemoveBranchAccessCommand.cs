using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Access {
    public record RemoveBranchAccessCommand(long UserId, long BranchId) 
        : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Access";
        public string AuditAction => "RemoveBranchAccess";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Delete;
        public string? AuditEntityName => "AppUser";
    }

}
