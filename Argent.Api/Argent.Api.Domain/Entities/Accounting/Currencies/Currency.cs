using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Argent.Api.Domain.Entities.Banking.Loans;

namespace Argent.Api.Domain.Entities.Accounting.Currencies {
    /// <summary>
    /// A currency used in the system. One currency is marked as BaseCurrency —
    /// all exchange rates and multi-currency conversions reference it.
    ///
    /// System = true means this currency was seeded by the system and cannot be deleted.
    /// </summary>
    public class Currency : BaseEntity {
        //e.g. "UGX", "USD", "KES" 
        public string Code { get; set; } = string.Empty;
        // e.g. "Uganda Shilling"
        public string Name { get; set; } = string.Empty;
        // e.g. "Cents", "Ngwe"
        public string? SmallUnit { get; set; }
        // e.g. "USh", "$"
        public string? Symbol { get; set; }
        // decimal places
        public int Precision { get; set; } = 2;
        // rounding factor
        public int Round { get; set; } = 0;                    

        /// <summary>
        /// The organization's home currency. Only one can be base at a time.
        /// </summary>
        public bool IsBaseCurrency { get; set; }

        public string? Country { get; set; }

        /// <summary>System-seeded currencies cannot be deleted.</summary>
        public bool IsSystem { get; set; }

        public ICollection<Denomination> Denominations { get; set; } = [];
        public ICollection<ExchangeRate> ExchangeRates { get; set; } = [];
        public ICollection<LedgerAccount> LedgerAccounts { get; set; } = [];
        public ICollection<BankAccountCurrency> BankAccounts { get; set; } = [];
        public ICollection<RevolvingFund> RevolvingFunds { get; set; } = [];
    }

}
