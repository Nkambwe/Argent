using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Settings {
    /// <summary>
    /// Typed configuration for a SavingProduct.
    /// Created automatically (seeded from defaults) when a SavingProduct is created.
    /// All string ledger fields are replaced by ProductPostingAccount rows.
    ///
    /// Covers:
    ///   - Interest calculation rules
    ///   - Overdraft settings
    ///   - Dormancy rules
    ///   - Cheque book settings
    ///   - Standing order settings
    ///   - Electronic card settings
    ///   - Per-segment minimum balances
    ///   - Withdrawal rules
    /// </summary>
    public class SavingProductConfiguration : BaseEntity {
        public long SavingProductId { get; set; }
        public SavingProduct SavingProduct { get; set; } = null!;

        #region Interest

        [ConfigurationParam("InterestBasedProduct", "Whether product offers interest on savings", "bool")]
        public bool InterestBasedProduct { get; set; }

        [ConfigurationParam("InterestRate", "Annual savings interest rate", "decimal")]
        public decimal InterestRate { get; set; }

        [ConfigurationParam("InterestDays", "Number of interest earning days in a year", "int")]
        public int InterestDays { get; set; } = 365;

        [ConfigurationParam("InterestWeeks", "Number of interest earning weeks in a year", "int")]
        public int InterestWeeks { get; set; } = 52;

        [ConfigurationParam("InterestMethod", "Savings interest calculation method", "int")]
        public SavingInterestCalculation InterestMethod { get; set; }

        [ConfigurationParam("OfferInterestOnDormantAccounts", "Whether dormant accounts earn interest", "bool")]
        public bool OfferInterestOnDormantAccounts { get; set; }

        [ConfigurationParam("ChargeWithholdingTaxOnSavingInterest", "Whether withholding tax is charged on interest earned", "bool")]
        public bool ChargeWithholdingTaxOnSavingInterest { get; set; }

        public DateTime? LastInterestCalculationDate { get; set; }
        public DateTime? LastRewardBonusCalculationDate { get; set; }

        #endregion

        #region Overdraft

        [ConfigurationParam("TurnOnOverdraftProtection", "Whether overdraft protection is enabled", "bool")]
        public bool TurnOnOverdraftProtection { get; set; } = true;

        [ConfigurationParam("OverdraftPeriod", "Overdraft period in days", "int")]
        public int OverdraftPeriod { get; set; } = 30;

        [ConfigurationParam("OverdraftInterestRate", "Overdraft interest rate", "decimal")]
        public decimal OverdraftInterestRate { get; set; }

        [ConfigurationParam("ChargeCommissionOnOverdraft", "Whether commission is charged on overdraft", "bool")]
        public bool ChargeCommissionOnOverdraft { get; set; }

        [ConfigurationParam("ChargeInterestOnNegativeBalances", "Whether interest is charged on negative balances", "bool")]
        public bool ChargeInterestOnNegativeBalances { get; set; }

        [ConfigurationParam("NegativeBalanceInterestRate", "Rate charged on negative balance accounts", "decimal")]
        public decimal NegativeBalanceInterestRate { get; set; }

        [ConfigurationParam("MinimumInterestOnNegativeBalance", "Minimum interest on negative balance", "decimal")]
        public decimal MinimumInterestOnNegativeBalance { get; set; }

        #endregion

        #region Dormancy

        [ConfigurationParam("ConsiderDormantAfterDaysOfInactivity", "Days of inactivity before account is dormant", "int")]
        public int ConsiderDormantAfterDaysOfInactivity { get; set; } = 365;

        [ConfigurationParam("RequireSupervisorApprovalToActivateDormantAccounts", "Whether supervisor approval is needed to reactivate dormant accounts", "bool")]
        public bool RequireSupervisorApprovalToActivateDormantAccounts { get; set; }

        [ConfigurationParam("TrackDormantGroupAccountsPerMember", "Whether dormant group accounts are tracked per member", "bool")]
        public bool TrackDormantGroupAccountsPerMember { get; set; }

        #endregion

        #region Withdrawals

        [ConfigurationParam("RequireApprovalForWithdraws", "Whether supervisor approval is required for withdrawals", "bool")]
        public bool RequireApprovalForWithdraws { get; set; }

        [ConfigurationParam("RequireApprovalForWithdrawsAboveCashierLimit", "Whether approval is required for withdrawals above cashier limit", "bool")]
        public bool RequireApprovalForWithdrawsAboveCashierLimit { get; set; }

        [ConfigurationParam("ChargeWithdrawCommission", "Whether withdraw commission is charged", "bool")]
        public bool ChargeWithdrawCommission { get; set; }

        [ConfigurationParam("UseWithdrawCommissionRange", "Whether commission ranges apply on withdrawals", "bool")]
        public bool UseWithdrawCommissionRange { get; set; }

        [ConfigurationParam("WithdrawInterval", "Minimum days between two withdrawals", "int")]
        public int WithdrawInterval { get; set; }

        [ConfigurationParam("ChargePenaltyForWithdrawInterval", "Whether penalty applies for withdrawals before interval", "bool")]
        public bool ChargePenaltyForWithdrawInterval { get; set; }

        #endregion

        #region Cheque book

        [ConfigurationParam("HasChequeBook", "Whether product supports cheque books", "bool")]
        public bool HasChequeBook { get; set; }

        [ConfigurationParam("NumberOfLeafs", "Number of leaves in a cheque book", "int")]
        public int NumberOfLeafs { get; set; }

        [ConfigurationParam("ChargePerLeaf", "Whether cheque book is charged per leaf", "bool")]
        public bool ChargePerLeaf { get; set; }

        [ConfigurationParam("ChequeBookCharge", "Cheque book charge amount", "decimal")]
        public decimal ChequeBookCharge { get; set; }

        [ConfigurationParam("AllowChequeDeposit", "Whether cheque deposits are allowed", "bool")]
        public bool AllowChequeDeposit { get; set; }

        [ConfigurationParam("ChargeCommissionOnCheques", "Whether commission is charged on cheques", "bool")]
        public bool ChargeCommissionOnCheques { get; set; }

        #endregion

        #region Standing orders

        [ConfigurationParam("AutoExecuteStandingOrdersAtStartOfDay", "Whether standing orders execute automatically at start of day", "bool")]
        public bool AutoExecuteStandingOrdersAtStartOfDay { get; set; }

        [ConfigurationParam("ChargeInvocationFee", "Whether invocation fees are charged on standing orders", "bool")]
        public bool ChargeInvocationFee { get; set; }

        [ConfigurationParam("ChargeOrderExecutionFee", "Whether execution fees are charged on standing orders", "bool")]
        public bool ChargeOrderExecutionFee { get; set; }

        [ConfigurationParam("ChargeStandingOrderAmendmentFee", "Whether amendment fees are charged on standing orders", "bool")]
        public bool ChargeStandingOrderAmendmentFee { get; set; }

        [ConfigurationParam("ChargePenaltyStandingOrder", "Whether penalty applies on standing orders", "bool")]
        public bool ChargePenaltyStandingOrder { get; set; }

        #endregion

        #region Electronic and SMS banking 

        [ConfigurationParam("EnableSmsBanking", "Whether SMS banking is enabled", "bool")]
        public bool EnableSmsBanking { get; set; }

        [ConfigurationParam("MaximumAmountPerSmsTransaction", "Maximum amount per SMS transaction", "decimal")]
        public decimal MaximumAmountPerSmsTransaction { get; set; }

        [ConfigurationParam("EnableElectronicCardTransaction", "Whether electronic card transactions are enabled", "bool")]
        public bool EnableElectronicCardTransaction { get; set; }

        [ConfigurationParam("ElectronicCardNumberLength", "Length of electronic card number", "int")]
        public int ElectronicCardNumberLength { get; set; } = 10;

        [ConfigurationParam("ElectronicCardValidityInYears", "Card validity in years", "int")]
        public int ElectronicCardValidityInYears { get; set; }

        [ConfigurationParam("ChargeExerciseDutyOnElectronicCards", "Whether exercise duty is charged on electronic cards", "bool")]
        public bool ChargeExerciseDutyOnElectronicCards { get; set; }

        [ConfigurationParam("ElectronicCardWithdrawLimit", "Maximum withdrawal per card transaction", "decimal")]
        public decimal ElectronicCardWithdrawLimit { get; set; }

        [ConfigurationParam("ElectronicCardPurchaseLimit", "Maximum purchase per card transaction", "decimal")]
        public decimal ElectronicCardPurchaseLimit { get; set; }

        #endregion

        #region Per-segment rules

        [ConfigurationParam("EnforceIndividualSaving", "Whether individual savings are enforced", "bool")]
        public bool EnforceIndividualSaving { get; set; }

        [ConfigurationParam("MinimumBalanceIndividualAccounts", "Minimum balance for individual accounts", "decimal")]
        public decimal MinimumBalanceIndividualAccounts { get; set; }

        [ConfigurationParam("MinimumInterestEarningBalanceIndividualAccounts", "Minimum balance to earn interest for individuals", "decimal")]
        public decimal MinimumInterestEarningBalanceIndividualAccounts { get; set; }

        [ConfigurationParam("EnforceGroupSaving", "Whether group savings are enforced", "bool")]
        public bool EnforceGroupSaving { get; set; }

        [ConfigurationParam("BreakGroupAccountsToIndividualMemberAccounts", "Whether group accounts are broken down per member", "bool")]
        public bool BreakGroupAccountsToIndividualMemberAccounts { get; set; }

        [ConfigurationParam("MinimumBalanceGroupAccounts", "Minimum balance for group accounts", "decimal")]
        public decimal MinimumBalanceGroupAccounts { get; set; }

        [ConfigurationParam("MinimumInterestEarningBalanceGroupAccounts", "Minimum balance to earn interest for groups", "decimal")]
        public decimal MinimumInterestEarningBalanceGroupAccounts { get; set; }

        [ConfigurationParam("EnforceBusinessSaving", "Whether business savings are enforced", "bool")]
        public bool EnforceBusinessSaving { get; set; }

        [ConfigurationParam("MinimumBalanceBusinessAccounts", "Minimum balance for business accounts", "decimal")]
        public decimal MinimumBalanceBusinessAccounts { get; set; }

        [ConfigurationParam("MinimumInterestEarningBalanceBusinessAccounts", "Minimum balance to earn interest for businesses", "decimal")]
        public decimal MinimumInterestEarningBalanceBusinessAccounts { get; set; }

        #endregion

        #region Operational

        [ConfigurationParam("BookSavingsToGeneralLedger", "Whether savings transactions are booked to GL", "bool")]
        public bool BookSavingsToGeneralLedger { get; set; } = true;

        [ConfigurationParam("AllowMultiCurrency", "Whether multi-currency transactions are allowed", "bool")]
        public bool AllowMultiCurrency { get; set; }

        [ConfigurationParam("DisplayChronologically", "Whether transactions are displayed latest first", "bool")]
        public bool DisplayChronologically { get; set; }

        [ConfigurationParam("ShowCurrencyNotesOnDepositSlips", "Whether currency denominations show on deposit slips", "bool")]
        public bool ShowCurrencyNotesOnDepositSlips { get; set; }

        [ConfigurationParam("ShowCurrencyNotesOnWithdrawSlips", "Whether currency denominations show on withdrawal slips", "bool")]
        public bool ShowCurrencyNotesOnWithdrawSlips { get; set; }

        [ConfigurationParam("ChargeSavingsTransferFees", "Whether savings transfer fees are charged", "bool")]
        public bool ChargeSavingsTransferFees { get; set; }

        [ConfigurationParam("ChargeAccountOpeningFees", "Whether account opening fees are charged", "bool")]
        public bool ChargeAccountOpeningFees { get; set; }

        [ConfigurationParam("ChargeAccountClosureFees", "Whether account closure fees are charged", "bool")]
        public bool ChargeAccountClosureFees { get; set; }

        [ConfigurationParam("ChargeStationeryFees", "Whether stationery fees are charged", "bool")]
        public bool ChargeStationeryFees { get; set; }

        [ConfigurationParam("MinimumClientAge", "Minimum client age for this product", "int")]
        public int MinimumClientAge { get; set; }

        #endregion

    }

}
