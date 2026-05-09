using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting;

namespace Argent.Api.Domain.Entities.Banking {

    public class TellerLedgerAccount : BaseEntity {
        public long TellerId { get; set; }
        public virtual Teller Teller { get; set; } = null!;
        public long LegderAccountId { get; set; }
        public virtual LedgerAccount? LedgerAccount { get; set; } = null!;
    }

}
