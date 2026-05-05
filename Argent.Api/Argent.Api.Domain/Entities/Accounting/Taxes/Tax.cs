using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Charges;

namespace Argent.Api.Domain.Entities.Accounting.Taxes {
    public class Tax : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Get or Set whether tax is charged as percentage rate
        /// </summary>
        public bool IsRated { get; set; }
        /// <summary>
        /// Get or Set percentage rate charge
        /// </summary>
        public virtual decimal Rate { get; set; }
        /// <summary>
        /// Get or set flat rate charged
        /// </summary>
        public virtual decimal FlatAmount { get; set; }
        public bool Suspend { get; set; }
        public string Notes { get; set; } = string.Empty;

        public long TaxGroupId { get; set; }
        public virtual TaxGroup? TaxGroup { get; set; }
        public virtual ICollection<VendorTax> Vendors { get; set; } = [];
        public virtual ICollection<TaxableItem> TaxableItems { get; set; } = [];
        public virtual ICollection<ChargeItem> ChargedItems { get; set; } = [];

    }

}
