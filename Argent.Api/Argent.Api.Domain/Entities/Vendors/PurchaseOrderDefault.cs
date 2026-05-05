using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;

namespace Argent.Api.Domain.Entities.Vendors {
    public class PurchaseOrderDefault : BaseEntity {
        /// <summary>
        /// Get/Set bank account used for central payments in cases
        /// where the main branch pays for all branch purchases
        /// </summary>
        public string MultiBranchAccount { get; set; } = string.Empty;

        public long? VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }

        public long? VendorGroupId { get; set; }
        public virtual VendorGroup? VendorGroup { get; set; }

        public long? VendorItemGroupId { get; set; }
        public virtual VendorItemGroup? VendorItemGroup { get; set; }

        public long? DiscountGroupId { get; set; }
        public virtual DiscountGroup? DiscountGroup { get; set; }

        public long? PriceGroupId { get; set; }
        public virtual PriceGroup? PriceGroup { get; set; }

        public long? PurchaseOrderClassificationId { get; set; }
        public virtual PurchaseOrderClassification? PurchaseOrderClassification { get; set; }

        public long? BankAccountId { get; set; }
        public virtual BankAccount? BankAccount { get; set; }

    }
}
