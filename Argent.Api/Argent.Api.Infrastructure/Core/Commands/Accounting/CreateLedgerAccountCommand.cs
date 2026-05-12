using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public record CreateLedgerAccountCommand(CreateLedgerAccountRequest Request)
        : IRequest<Result<LedgerAccountDto>>, IAuditableCommand {
        public string AuditModule => "Accounting";
        public string AuditAction => "CreateLedgerAccount";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "LedgerAccount";
    }

}
