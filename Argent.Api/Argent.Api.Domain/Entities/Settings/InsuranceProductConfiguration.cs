using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Settings {
    /// <summary>
    /// Typed configuration for an InsuranceProduct.
    /// </summary>
    public class InsuranceProductConfiguration : BaseEntity {
        public long InsuranceProductId { get; set; }
        public InsuranceProduct InsuranceProduct { get; set; } = null!;

        [ConfigurationParam("PolicyPeriod", "Duration of each policy (months)", "int")]
        public int PolicyPeriod { get; set; }

        [ConfigurationParam("MinimumNumberInsured", "Minimum number of insured persons or assets", "int")]
        public int MinimumNumberInsured { get; set; }

        [ConfigurationParam("MaximumNumberInsured", "Maximum number of insured persons or assets", "int")]
        public int MaximumNumberInsured { get; set; }

        [ConfigurationParam("MinimumInsurableAge", "Minimum age for insured persons", "int")]
        public int MinimumInsurableAge { get; set; }

        [ConfigurationParam("MaximumInsurableAge", "Maximum age for insured persons", "int")]
        public int MaximumInsurableAge { get; set; }

        [ConfigurationParam("MonthlyPremium", "Monthly premium amount", "decimal")]
        public decimal MonthlyPremium { get; set; }

        [ConfigurationParam("PremiumPercentageCharged", "Premium as a percentage of insured value", "decimal")]
        public decimal PremiumPercentageCharged { get; set; }

        [ConfigurationParam("ChargeFixedAmount", "Whether premium is a fixed amount per insured person or asset", "bool")]
        public bool ChargeFixedAmount { get; set; }

        [ConfigurationParam("FixedAmount", "Fixed amount per insured person or asset", "decimal")]
        public decimal FixedAmount { get; set; }

        [ConfigurationParam("CanModifyPremium", "Whether agent can modify premium at policy registration", "bool")]
        public bool CanModifyPremium { get; set; }

        [ConfigurationParam("MinimumCoverage", "Minimum coverage amount", "decimal")]
        public decimal MinimumCoverage { get; set; }

        [ConfigurationParam("MaximumCoverage", "Maximum coverage amount", "decimal")]
        public decimal MaximumCoverage { get; set; }

        [ConfigurationParam("Discount", "Discount rate on policies", "decimal")]
        public decimal Discount { get; set; }

        [ConfigurationParam("ClaimsPercentage", "Percentage of premium allocated to claims fund", "decimal")]
        public decimal ClaimsPercentage { get; set; }

        [ConfigurationParam("AdministrationCostPercentage", "Percentage of premium for administration costs", "decimal")]
        public decimal AdministrationCostPercentage { get; set; }

        [ConfigurationParam("AdministrationFundPercentage", "Percentage of premium for administration fund", "decimal")]
        public decimal AdministrationFundPercentage { get; set; }

        [ConfigurationParam("ChargeWithholdingTaxOnCharges", "Whether withholding tax is charged on insurance charges", "bool")]
        public bool ChargeWithholdingTaxOnCharges { get; set; }

        [ConfigurationParam("ChargeStampDutyOnPolicies", "Whether stamp duty applies on insurance policies", "bool")]
        public bool ChargeStampDutyOnPolicies { get; set; }

        [ConfigurationParam("RequireApprovalForClaims", "Whether claims require management approval", "bool")]
        public bool RequireApprovalForClaims { get; set; }

        [ConfigurationParam("WaitingPeriodDays", "Days after policy activation before claims are accepted", "int")]
        public int WaitingPeriodDays { get; set; }
    }


}
