using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Vendors {
    public class DeliveryDefaults : BaseEntity {
        /// <summary>
        /// Get/Set person receiving the delivery
        /// </summary>
        public string Receiver { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set reference group for sales on this delivery
        /// </summary>
        public string ReferenceGroup { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set reference value for sales on this delivery
        /// </summary>
        public string ReferenceValue { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set area where customer or vendor operates business
        /// </summary>
        public string DeliveryAddress { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set default transaction currency
        /// </summary>
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// Get/Set delivery notes
        /// </summary>
        public string Notes { get; set; } = string.Empty;

        public long? VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }
}
