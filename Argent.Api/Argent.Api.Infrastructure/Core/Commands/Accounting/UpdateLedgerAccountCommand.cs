using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public record UpdateLedgerAccountCommand(long AccountId, UpdateLedgerAccountRequest Request)
        : IRequest<Result<LedgerAccountDto>>, IAuditableCommand {
        public string AuditModule => "Accounting";
        public string AuditAction => "UpdateLedgerAccount";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "LedgerAccount";
    }

}
