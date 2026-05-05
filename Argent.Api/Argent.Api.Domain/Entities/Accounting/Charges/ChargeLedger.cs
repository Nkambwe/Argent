using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    public class ChargeLedger : BaseEntity {
        public string TransactionCode { get; set; } = string.Empty;
        public DateTime PostedOn { get; set; }
        public string Series { get; set; } = string.Empty;
        public string Client { get; set; } = string.Empty;
        public string LoanNumber { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public string LedgerNumber { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public long ChargeItemId { get; set; }
        public virtual ChargeItem? ChargeItem { get; set; }
    }


}
