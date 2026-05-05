using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Charges;

namespace Argent.Api.Domain.Entities.Accounting {
    public class RegistrationLedgerEntry : BaseEntity {
        public string TransactionCode { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string ClientCode { get; set; } = string.Empty;
        public DateTime PostedOn { get; set; }
        public long ChargeItemId { get; set; }
        public virtual ChargeItem? ChargeItem { get; set; }
    }

}
