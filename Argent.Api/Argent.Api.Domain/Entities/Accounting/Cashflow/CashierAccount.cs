using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// Junction: which cashiers can operate which cash accounts.
    /// </summary>
    public class CashierAccount : BaseEntity {
        public long CashierId { get; set; }
        public Cashier Cashier { get; set; } = null!;
        public long CashAccountId { get; set; }
        public CashAccount CashAccount { get; set; } = null!;
    }
}
