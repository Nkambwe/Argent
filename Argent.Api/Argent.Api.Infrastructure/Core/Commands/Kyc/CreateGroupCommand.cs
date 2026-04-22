using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Kyc {

    public record CreateGroupCommand(CreateGroupRequest Request,long TargetBranchId) : IRequest<Result<GroupDto>>, IAuditableCommand, IBranchPolicyCommand {
        public string AuditModule => "CustomerKyc";
        public string AuditAction => "CreateGroup";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "Group";
        public string RequiredPermission => "CustomerKyc.CreateGroup";
    }
}
