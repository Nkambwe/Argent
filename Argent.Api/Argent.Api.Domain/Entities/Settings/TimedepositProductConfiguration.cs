using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Settings {
    /// <summary>
    /// Typed configuration for a TimedepositProduct.
    /// </summary>
    public class TimedepositProductConfiguration : BaseEntity {
        public long TimedepositProductId { get; set; }
        public TimedepositProduct TimedepositProduct { get; set; } = null!;

        [ConfigurationParam("MinimumProductAmount", "Minimum deposit amount", "decimal")]
        public decimal MinimumProductAmount { get; set; }

        [ConfigurationParam("MaximumProductAmount", "Maximum deposit amount", "decimal")]
        public decimal MaximumProductAmount { get; set; }

        [ConfigurationParam("MinimumInterestRate", "Minimum applicable interest rate", "decimal")]
        public decimal MinimumInterestRate { get; set; }

        [ConfigurationParam("MaximumInterestRate", "Maximum applicable interest rate", "decimal")]
        public decimal MaximumInterestRate { get; set; }

        [ConfigurationParam("MinimumInterestPeriod", "Minimum deposit period", "int")]
        public int MinimumInterestPeriod { get; set; }

        [ConfigurationParam("MaximumInterestPeriod", "Maximum deposit period", "int")]
        public int MaximumInterestPeriod { get; set; }

        [ConfigurationParam("InterestPeriodInDays", "Interest calculation period expressed in days", "int")]
        public int InterestPeriodInDays { get; set; } = 365;

        [ConfigurationParam("PenaltyAmount", "Fixed penalty amount for premature withdrawal", "decimal")]
        public decimal PenaltyAmount { get; set; }

        [ConfigurationParam("UsePercentageBasedPenalty", "Whether penalty is percentage-based rather than flat", "bool")]
        public bool UsePercentageBasedPenalty { get; set; }

        [ConfigurationParam("PenaltyRate", "Penalty percentage rate for premature withdrawal", "decimal")]
        public decimal PenaltyRate { get; set; }

        [ConfigurationParam("NoInterestOnPrematureWithdraw", "Whether interest is forfeited entirely on premature withdrawal", "bool")]
        public bool NoInterestOnPrematureWithdraw { get; set; }

        [ConfigurationParam("ChargeWithholdingTaxOnInterest", "Whether withholding tax is charged on time deposit interest", "bool")]
        public bool ChargeWithholdingTaxOnInterest { get; set; }

        [ConfigurationParam("AutoRenewOnMaturity", "Whether deposit auto-renews at maturity if not claimed", "bool")]
        public bool AutoRenewOnMaturity { get; set; }

        [ConfigurationParam("RequireApprovalForPrematureWithdrawal", "Whether premature withdrawal requires approval", "bool")]
        public bool RequireApprovalForPrematureWithdrawal { get; set; }
    }

}
