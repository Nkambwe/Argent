using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public record DeleteLedgerAccountCommand(long AccountId)
        : IRequest<Result>, IAuditableCommand {
        public string AuditModule => "Accounting";
        public string AuditAction => "DeleteLedgerAccount";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Delete;
        public string? AuditEntityName => "LedgerAccount";
    }

}
