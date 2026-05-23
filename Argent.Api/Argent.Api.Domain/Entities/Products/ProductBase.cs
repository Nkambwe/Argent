using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Entities.Banking.Loans;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// Abstract base for all five product modules.
    ///
    /// UseChargeGroups: when true, charges are drawn from the linked ChargeGroup
    /// rather than individual ChargeItem/ChargeStage records.
    ///
    /// VatInclusive: whether product prices already include VAT.
    ///
    /// IsActive: inactive products cannot be used to open new accounts.
    /// Existing accounts on an inactive product continue to operate normally.
    /// </summary>
    public abstract class ProductBase : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public bool VatInclusive { get; set; }
        public bool UseChargeGroups { get; set; }
        public string? Description { get; set; }

        /// <summary>Which module this product belongs to — set by each concrete type.</summary>
        public abstract ProductModuleType Module { get; }
    }

}
