using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    /// <summary>
    /// M:M junction between ChargeItem and Charge.
    /// A charge item can participate in multiple charge definitions.
    /// </summary>
    public class ChargeItemCharge : BaseEntity {
        public long ChargeItemId { get; set; }
        public ChargeItem ChargeItem { get; set; } = null!;
        public long ChargeId { get; set; }
        public Charge Charge { get; set; } = null!;
    }

}
