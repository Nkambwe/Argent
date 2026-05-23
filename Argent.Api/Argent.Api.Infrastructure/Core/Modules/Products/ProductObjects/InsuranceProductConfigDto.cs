namespace Argent.Api.Infrastructure.Core.Modules.Products.ProductObjects {
    public class InsuranceProductConfigDto {
        public int PolicyPeriod { get; set; }
        public int MinimumNumberInsured { get; set; }
        public int MaximumNumberInsured { get; set; }
        public int MinimumInsurableAge { get; set; }
        public int MaximumInsurableAge { get; set; }
        public decimal MonthlyPremium { get; set; }
        public decimal PremiumPercentageCharged { get; set; }
        public bool ChargeFixedAmount { get; set; }
        public decimal FixedAmount { get; set; }
        public bool CanModifyPremium { get; set; }
        public decimal MinimumCoverage { get; set; }
        public decimal MaximumCoverage { get; set; }
        public decimal Discount { get; set; }
        public decimal ClaimsPercentage { get; set; }
        public decimal AdministrationCostPercentage { get; set; }
        public decimal AdministrationFundPercentage { get; set; }
        public bool ChargeWithholdingTaxOnCharges { get; set; }
        public bool ChargeStampDutyOnPolicies { get; set; }
        public bool RequireApprovalForClaims { get; set; }
        public int WaitingPeriodDays { get; set; }
    }

}
