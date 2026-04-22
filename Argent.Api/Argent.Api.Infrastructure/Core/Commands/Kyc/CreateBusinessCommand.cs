using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Kyc {
    public record CreateBusinessCommand(CreateBusinessRequest Request, long TargetBranchId) : IRequest<Result<BusinessDto>>, IAuditableCommand, IBranchPolicyCommand {
        public string AuditModule => "CustomerKyc";
        public string AuditAction => "CreateBusiness";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "Business";
        public string RequiredPermission => "CustomerKyc.CreateBusiness";
    }
}
