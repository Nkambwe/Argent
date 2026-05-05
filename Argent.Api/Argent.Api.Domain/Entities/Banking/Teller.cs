using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Access;

namespace Argent.Api.Domain.Entities.Banking {

    public class Teller: BaseEntity {
        public string TellerCode { get; set; } = string.Empty;
        public decimal MaximumLimit { get; set; }
        public decimal MinimumLimit { get; set; }
        public long AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual ICollection<TellerLedgerAccount> TellerLedgerAccounts { get; set; } = [];
    }

}
