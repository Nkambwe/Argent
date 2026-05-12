using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public record SuspendLedgerAccountCommand(long AccountId)
        : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Accounting";
        public string AuditAction => "SuspendLedgerAccount";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "LedgerAccount";
    }

}
