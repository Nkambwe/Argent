using Argent.Api.Domain.Enums;
using Argent.Api.Infrastructure.Core.Common;
using Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects;
using MediatR;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public record GetLedgerAccountsQuery(LedgerSearchRequest Request)
        : IRequest<Result<PagedResult<LedgerAccountDto>>> {
        public string AuditModule => "Accounting";
        public string AuditAction => "RetrieveLedgerAccount";
        public AuditAction AuditActionType => Domain.Enums.AuditAction.Create;
        public string? AuditEntityName => "LedgerAccount";
    }
}
