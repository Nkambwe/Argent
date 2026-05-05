using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Vendors {
    public class InvoicingDefault : BaseEntity {
        /// <summary>
        /// Invoice account in cases of multi-branch sales
        /// </summary>
        public string MultiBranchInvoiceAccount { get; set; } = string.Empty;
        public string InvoicingLedger { get; set; } = string.Empty;
        public string InvoicingAddress { get; set; } = string.Empty;
        public bool PriceIncludesSalesTax { get; set; }
        public bool PriceIncludesWithHoldingTax { get; set; }
        public bool PriceIncludesVat { get; set; }
        public long? VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }
    }
}
