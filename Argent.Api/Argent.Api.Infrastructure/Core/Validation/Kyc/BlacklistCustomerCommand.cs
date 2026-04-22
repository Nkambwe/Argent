using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public record BlacklistCustomerCommand(long CustomerId, CustomerType CustomerType, long ReasonId, string? Notes, long TargetBranchId) 
        : IRequest<Result>, IAuditableCommand, IBranchPolicyCommand {
        public string AuditModule => "CustomerKyc";
        public string AuditAction => "BlacklistCustomer";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => CustomerType.ToString();
        public string RequiredPermission => "CustomerKyc.BlacklistCustomer";
    }


}
