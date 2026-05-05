using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Charges;

namespace Argent.Api.Domain.Entities.Accounting {
    public class ChargeLedgerEntry : BaseEntity {
        public string TransactionCode { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string Customer { get; set; } = string.Empty;
        public string LoanNumber { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string LedgerNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime PostedOn { get; set; }
        public long ChargeItemId { get; set; }
        public virtual ChargeItem? ChargeItem { get; set; }
    }

}
