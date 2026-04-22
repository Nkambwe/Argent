using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Kyc {

    public record CreateIndividualCommand(CreateIndividualRequest Request, long TargetBranchId) : IRequest<Result<IndividualDto>>, IAuditableCommand, IBranchPolicyCommand {
        // IAuditableCommand
        public string AuditModule => "CustomerKyc";
        public string AuditAction => "CreateIndividual";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "Individual";

        // IBranchPolicyCommand
        public string RequiredPermission => "CustomerKyc.CreateIndividual";
    }

}
