using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    public class CashAccount : BaseEntity {
        /// <summary>
        /// GL ledger number this cash account posts to.
        /// </summary>
        public string LedgerNumber { get; set; } = string.Empty;
        public long LedgerAccountId { get; set; }
        public LedgerAccount LedgerAccount { get; set; } = null!;

        public decimal MinimumPayout { get; set; }
        public decimal MaximumPayout { get; set; }
        public bool AllowMultiCurrency { get; set; }

        /// <summary>
        /// Cashiers authorized to operate this cash account
        /// </summary>
        public ICollection<CashierAccount> CashierAccounts { get; set; } = [];
    }
}
