using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class UpdateSavingProductConfigRequest {
        public bool InterestBasedProduct { get; set; }
        public decimal InterestRate { get; set; }
        public int InterestDays { get; set; }
        public int InterestWeeks { get; set; }
        public SavingInterestCalculation InterestMethod { get; set; }
        public bool OfferInterestOnDormantAccounts { get; set; }
        public bool ChargeWithholdingTaxOnSavingInterest { get; set; }
        public bool TurnOnOverdraftProtection { get; set; }
        public int OverdraftPeriod { get; set; }
        public decimal OverdraftInterestRate { get; set; }
        public bool ChargeCommissionOnOverdraft { get; set; }
        public bool ChargeInterestOnNegativeBalances { get; set; }
        public decimal NegativeBalanceInterestRate { get; set; }
        public decimal MinimumInterestOnNegativeBalance { get; set; }
        public int ConsiderDormantAfterDaysOfInactivity { get; set; }
        public bool RequireSupervisorApprovalToActivateDormantAccounts { get; set; }
        public bool RequireApprovalForWithdraws { get; set; }
        public bool RequireApprovalForWithdrawsAboveCashierLimit { get; set; }
        public bool ChargeWithdrawCommission { get; set; }
        public bool UseWithdrawCommissionRange { get; set; }
        public int WithdrawInterval { get; set; }
        public bool ChargePenaltyForWithdrawInterval { get; set; }
        public bool HasChequeBook { get; set; }
        public int NumberOfLeafs { get; set; }
        public bool ChargePerLeaf { get; set; }
        public decimal ChequeBookCharge { get; set; }
        public bool AllowChequeDeposit { get; set; }
        public bool AutoExecuteStandingOrdersAtStartOfDay { get; set; }
        public bool EnableSmsBanking { get; set; }
        public decimal MaximumAmountPerSmsTransaction { get; set; }
        public bool EnableElectronicCardTransaction { get; set; }
        public decimal ElectronicCardWithdrawLimit { get; set; }
        public decimal ElectronicCardPurchaseLimit { get; set; }
        public bool BookSavingsToGeneralLedger { get; set; }
        public bool AllowMultiCurrency { get; set; }
        public bool EnforceIndividualSaving { get; set; }
        public decimal MinimumBalanceIndividualAccounts { get; set; }
        public decimal MinimumInterestEarningBalanceIndividualAccounts { get; set; }
        public bool EnforceGroupSaving { get; set; }
        public bool BreakGroupAccountsToIndividualMemberAccounts { get; set; }
        public decimal MinimumBalanceGroupAccounts { get; set; }
        public decimal MinimumInterestEarningBalanceGroupAccounts { get; set; }
        public bool EnforceBusinessSaving { get; set; }
        public decimal MinimumBalanceBusinessAccounts { get; set; }
        public decimal MinimumInterestEarningBalanceBusinessAccounts { get; set; }
        public bool ChargeAccountOpeningFees { get; set; }
        public bool ChargeAccountClosureFees { get; set; }
        public bool ChargeSavingsTransferFees { get; set; }
        public int MinimumClientAge { get; set; }
    }

}
