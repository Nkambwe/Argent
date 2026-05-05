using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    /// <summary>
    /// A specific charge entry within a ChargeGroup.
    /// Can be rate-based (percentage of transaction amount) or flat amount.
    /// </summary>
    public class ChargeGroupItem : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string ChargeName { get; set; } = string.Empty;
        public bool IsRated { get; set; }
        public decimal Rate { get; set; }           // percentage when IsRated = true
        public decimal FlatAmount { get; set; }     // fixed amount when IsRated = false
        public string? Notes { get; set; }

        public long ChargeGroupId { get; set; }
        public ChargeGroup ChargeGroup { get; set; } = null!;
    }


}
