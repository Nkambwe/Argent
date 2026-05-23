using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Settings {
    /// <summary>
    /// Typed configuration for a ShareProduct.
    /// Covers dividend calculation, GL accounts (via PostingAccounts),
    /// withholding tax, and share capital rules.
    /// </summary>
    public class ShareProductConfiguration : BaseEntity {
        public long ShareProductId { get; set; }
        public ShareProduct ShareProduct { get; set; } = null!;

        [ConfigurationParam("NominalValue", "Share nominal value (par value per share)", "decimal")]
        public decimal NominalValue { get; set; }

        [ConfigurationParam("MinimumShareCapital", "Minimum share capital contribution required", "decimal")]
        public decimal MinimumShareCapital { get; set; }

        [ConfigurationParam("DividendCalculationMethod", "Method used to calculate dividends", "int")]
        public DividendCalculationMethod DividendCalculationMethod { get; set; } = DividendCalculationMethod.None;

        [ConfigurationParam("DividendCalculationPeriod", "Period length for dividend calculation", "int")]
        public int DividendCalculationPeriod { get; set; }

        [ConfigurationParam("DividendCalculationInterval", "Interval type for dividend period (Days/Months/Years)", "int")]
        public IntervalType DividendCalculationInterval { get; set; } = IntervalType.Months;

        [ConfigurationParam("DividendRate", "Dividend calculation percentage rate", "decimal")]
        public decimal DividendRate { get; set; }

        [ConfigurationParam("DividendEarningShares", "Which shares earn dividends (e.g. ordinary only)", "string")]
        public string DividendEarningShares { get; set; } = string.Empty;

        public DateTime? LastDividendCalculationDate { get; set; }

        [ConfigurationParam("ChargeWithholdingTaxOnDividends", "Whether withholding tax is charged on dividends", "bool")]
        public bool ChargeWithholdingTaxOnDividends { get; set; }

        [ConfigurationParam("AllowShareRedemption", "Whether members can redeem shares", "bool")]
        public bool AllowShareRedemption { get; set; } = true;

        [ConfigurationParam("MinimumSharesAfterRedemption", "Minimum shares a member must retain after redemption", "int")]
        public int MinimumSharesAfterRedemption { get; set; }

        [ConfigurationParam("RequireApprovalForRedemption", "Whether redemption requires approval", "bool")]
        public bool RequireApprovalForRedemption { get; set; }
    }
}
