using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Vendors {
    public class PurchasingDefault : BaseEntity {
        /// <summary>
        /// Get/Set person to contact on purchase
        /// </summary>
        public string ContactPerson { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set reference group for purchases on this delivery
        /// </summary>
        public string ReferenceGroup { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set reference value for sales on this delivery
        /// </summary>
        public string ReferenceValue { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set employee responsible for the purchase
        /// </summary>
        public string PurchaseOfficer { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set default transaction currency
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set delivery notes
        /// </summary>
        public string Notes { get; set; } = string.Empty;
        public long VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }
}
