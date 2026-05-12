using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Common.Interfaces;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Commands.Accounting {
    public record UpdateLedgerHeaderCommand(long HeaderId, UpdateLedgerHeaderRequest Request)
        : IRequest<Result<LedgerAccountHeaderDto>>, IAuditableCommand {
        public string AuditModule => "Accounting";
        public string AuditAction => "UpdateLedgerHeader";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Update;
        public string? AuditEntityName => "LedgerAccountHeader";
    }

}
