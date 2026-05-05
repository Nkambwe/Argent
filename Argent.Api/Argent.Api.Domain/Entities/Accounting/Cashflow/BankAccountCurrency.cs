using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// Junction: currencies supported by a multi-currency bank account.
    /// </summary>
    public class BankAccountCurrency : BaseEntity {
        public long BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; } = null!;
        public long CurrencyId { get; set; }
        public Currencies.Currency Currency { get; set; } = null!;
    }

}
