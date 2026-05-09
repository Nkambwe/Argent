using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Entities.Banking.Loans;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// Share product
    /// </summary>
    public class ShareProduct : ProductBase {
        public long ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; } = null!;
        public long? ChargeGroupId { get; set; }
        public virtual ChargeGroup ChargeGroup { get; set; } = null!;
        public virtual ICollection<LoanChargeStage> ChargeStages { get; set; } = [];
        public virtual ICollection<ShareProductTaxGroup> TaxGroups { get; set; } = [];
        public virtual ICollection<TaxableItem> TaxableItems { get; set; } = [];
        public virtual ICollection<ShareProductChargeItem> ShareProductChargeItems { get; set; } = [];
        //public virtual ICollection<ShareProductParam> ProductParams { get; set; } = [];
        //public virtual ICollection<ShareAccount> ShareAccounts { get; set; } = [];
        //public virtual ICollection<ShareValue> ShareValues { get; set; } = [];
    }
}
