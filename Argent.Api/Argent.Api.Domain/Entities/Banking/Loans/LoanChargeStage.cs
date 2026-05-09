using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Banking.Loans {
    /// <summary>
    /// Defines at which lifecycle stage of a product a ChargeItem is applied.
    /// e.g. a loan processing fee applies BeforeApproval;
    ///      a disbursement fee applies AtDisbursement.
    /// Uses ProductReference to identify the product, avoiding nullable FK scatter.
    /// </summary>
    public class LoanChargeStage : BaseEntity {
        public long ChargeItemId { get; set; }
        public ChargeItem ChargeItem { get; set; } = null!;
        public long LoanProductId { get; set; }
        public LoanProduct LoanProduct { get; set; } = null!;
        public bool BeforeApplication { get; set; }
        public bool BeforeApproval { get; set; }
        public bool AfterApproval { get; set; }
        public bool AtDisbursement { get; set; }
        public bool AtAccountOpening { get; set; }
        public bool AtAccountClosure { get; set; }
        public bool Recurring { get; set; } 
    }

}
