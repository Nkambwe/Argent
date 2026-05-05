using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Vendors;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// A bank account the organization or its customers/vendors hold at a BankBranch.
    ///
    /// AccountFor distinguishes whether this account belongs to the organization itself,
    /// a customer, or a vendor — important for reconciliation and reporting.
    ///
    /// MultiCurrency: when true, this account can hold balances in multiple currencies
    /// (currencies tracked via BankAccountCurrency junction).
    ///
    /// ExcludeBranches: when true, this account is not visible/accessible to branch-level users.
    /// </summary>
    public class BankAccount : BaseEntity {
        /// <summary>Reference to the customer/vendor/organization holder — not a FK, a code lookup.</summary>
        public string? HolderCode { get; set; }

        [EncryptableAttribute("Account Name")]
        public string AccountName { get; set; } = string.Empty;

        [EncryptableAttribute("Account Number")]
        public string AccountNumber { get; set; } = string.Empty;

        public string? IbanNumber { get; set; }
        public string? SwiftNumber { get; set; }

        public AccountHolder AccountFor { get; set; }
        public Operation AllowedOperations { get; set; }
        public bool MultiCurrency { get; set; }

        /// <summary>
        /// Minimum days between withdrawals
        /// </summary>
        public int WithdrawInterval { get; set; }
        public Interval WithdrawIntervalUnit { get; set; }

        public bool HasChequeBook { get; set; }
        public bool Active { get; set; } = true;

        [EncryptableAttribute("Credit Limit")]
        public decimal CreditLimit { get; set; }

        /// <summary>
        /// When true, this account is not accessible to branch-level operators.
        /// </summary>
        public bool ExcludeBranches { get; set; }
        public long? LedgerAccountId { get; set; }
        public LedgerAccount? LedgerAccount { get; set; }
        public long BankBranchId { get; set; }
        public BankBranch BankBranch { get; set; } = null!;
        public ICollection<BankAccountCurrency> Currencies { get; set; } = [];
        public ICollection<ChequeBook> ChequeBooks { get; set; } = [];
        public ICollection<BankLedgerEntry> Transactions { get; set; } = [];
        public ICollection<VendorBankAccount> VendorAcounts { get; set; } = [];
        public ICollection<PaymentDefault> PaymentDefault { get; set; } = [];
        public ICollection<PurchaseOrderDefault> PurchaseOrderDefaults { get; set; } = [];
    }
}
