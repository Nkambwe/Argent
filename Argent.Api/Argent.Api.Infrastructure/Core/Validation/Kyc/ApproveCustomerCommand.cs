using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Validation.Kyc {
    public record ApproveCustomerCommand(long CustomerId, CustomerType CustomerType, string? Comments, long TargetBranchId) : 
        IRequest<Result<CustomerSummaryDto>>, IAuditableCommand, IBranchPolicyCommand {
        public string AuditModule => "CustomerKyc";
        public string AuditAction => "ApproveCustomer";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Approve;
        public string? AuditEntityName => CustomerType.ToString();
        public string RequiredPermission => "CustomerKyc.ApproveIndividual";
    }


}
