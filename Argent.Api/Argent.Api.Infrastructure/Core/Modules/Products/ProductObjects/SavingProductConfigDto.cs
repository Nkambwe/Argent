namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class SavingProductConfigDto {
        public bool InterestBasedProduct { get; set; }
        public decimal InterestRate { get; set; }
        public int InterestDays { get; set; }
        public string InterestMethod { get; set; } = string.Empty;
        public bool ChargeWithholdingTaxOnSavingInterest { get; set; }
        public bool TurnOnOverdraftProtection { get; set; }
        public decimal OverdraftInterestRate { get; set; }
        public int OverdraftPeriod { get; set; }
        public bool ChargeInterestOnNegativeBalances { get; set; }
        public int ConsiderDormantAfterDaysOfInactivity { get; set; }
        public bool HasChequeBook { get; set; }
        public bool EnableSmsBanking { get; set; }
        public bool EnableElectronicCardTransaction { get; set; }
        public bool BookSavingsToGeneralLedger { get; set; }
        public bool AllowMultiCurrency { get; set; }
        public bool EnforceIndividualSaving { get; set; }
        public decimal MinimumBalanceIndividualAccounts { get; set; }
        public bool EnforceGroupSaving { get; set; }
        public decimal MinimumBalanceGroupAccounts { get; set; }
        public bool EnforceBusinessSaving { get; set; }
        public decimal MinimumBalanceBusinessAccounts { get; set; }
        public int MinimumClientAge { get; set; }
    }

}
