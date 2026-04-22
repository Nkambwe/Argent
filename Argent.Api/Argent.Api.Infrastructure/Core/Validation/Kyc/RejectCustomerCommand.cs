using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public record RejectCustomerCommand(long CustomerId, CustomerType CustomerType, long ReasonId, string? Notes, long TargetBranchId) : IRequest<Result>, IAuditableCommand, IBranchPolicyCommand {
        public string AuditModule => "CustomerKyc";
        public string AuditAction => "RejectCustomer";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Reject;
        public string? AuditEntityName => CustomerType.ToString();
        public string RequiredPermission => "CustomerKyc.ApproveIndividual";
    }


}
