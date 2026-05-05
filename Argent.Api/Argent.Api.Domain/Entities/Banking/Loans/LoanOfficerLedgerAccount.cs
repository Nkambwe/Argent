using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting;

namespace Argent.Api.Domain.Entities.Banking.Loans {
    public class LoanOfficerLedgerAccount : BaseEntity {
        public long LoanOfficerId { get; set; }
        public virtual LoanOfficer? LoanOfficer { get; set; }
        public long LegderAccountId { get; set; }
        public virtual LedgerAccount? LedgerAccount { get; set; }
    }
}
