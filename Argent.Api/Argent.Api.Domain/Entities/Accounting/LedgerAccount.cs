using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Argent.Api.Domain.Entities.Accounting.Currencies;
using Argent.Api.Domain.Entities.Banking;
using Argent.Api.Domain.Entities.Banking.Loans;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting {
    /// <summary>
    /// A postable general ledger account.
    /// Every financial transaction in every module posts to a LedgerAccount.
    /// </summary>
    public class LedgerAccount : AccountBase {
        public NormalBalance NormalBalance { get; set; }
        public PostingType PostingType { get; set; }

        /// <summary>When false, only system-generated entries are allowed.</summary>
        public bool AllowManualPosting { get; set; }

        /// <summary>
        /// Show free-text description on journal entries for this account.
        /// </summary>
        public bool ShowParticulars { get; set; }

        public bool Suspended { get; set; }

        /// <summary>
        /// Current running balance. Updated on each posting.
        /// </summary>
        public decimal Balance { get; set; }

        public string? Notes { get; set; }

        public long LedgerAccountHeaderId { get; set; }
        public LedgerAccountHeader LedgerAccountHeader { get; set; } = null!;

        public long AccountsChartId { get; set; }
        public AccountsChart AccountsChart { get; set; } = null!;

        public long? FolioId { get; set; }
        public Folio? Folio { get; set; }

        public long? CurrencyId { get; set; }
        public Currency? Currency { get; set; }

        public virtual ICollection<TellerLedgerAccount> TellerLedgerAccounts { get; set; } = [];
        public virtual ICollection<LoanOfficerLedgerAccount> LoanOfficerLedgerAccounts { get; set; } = [];

        public ICollection<LedgerAccountReference> References { get; set; } = [];
        public ICollection<CashAccount> CashAccounts { get; set; } = [];
        public ICollection<BankAccount> BankAccounts { get; set; } = [];
        public ICollection<BranchLedgerAccount> BranchLedgerAccounts { get; set; } = [];
    }
}
