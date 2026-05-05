using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    /// <summary>
    /// Defines at which lifecycle stage of a product a ChargeItem is applied.
    /// e.g. a loan processing fee applies BeforeApproval;
    ///      a disbursement fee applies AtDisbursement.
    ///
    /// Uses ProductReference to identify the product, avoiding nullable FK scatter.
    /// </summary>
    public class ChargeStage : BaseEntity {
        public long ChargeItemId { get; set; }
        public ChargeItem ChargeItem { get; set; } = null!;
        public long? ProductId { get; set; }
        public ProductType? ProductType { get; set; }
        public bool BeforeApplication { get; set; }
        public bool BeforeApproval { get; set; }
        public bool AfterApproval { get; set; }
        public bool AtDisbursement { get; set; }
        public bool AtAccountOpening { get; set; }
        public bool AtAccountClosure { get; set; }
        public bool Recurring { get; set; } 
    }

}
