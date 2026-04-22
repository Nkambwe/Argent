using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Kyc {
    /// <summary>
    /// MEMBER 
    /// </summary>
    /// <param name="Request"></param>
    /// <param name="TargetBranchId"></param>
    /// <remarks>
    /// Added to an existing group
    /// </remarks>
    public record CreateMemberCommand(CreateMemberRequest Request, long TargetBranchId) : IRequest<Result<CustomerSummaryDto>>, IAuditableCommand, IBranchPolicyCommand {
        public string AuditModule => "CustomerKyc";
        public string AuditAction => "RegisterMember";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "Member";
        public string RequiredPermission => "CustomerKyc.ManageGroupMembers";
    }
}
