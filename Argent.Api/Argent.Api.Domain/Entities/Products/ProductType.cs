using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Charges;
using Argent.Api.Domain.Entities.Accounting.Taxes;
using Argent.Api.Domain.Entities.Banking.Loans;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Products {
    /// <summary>
    /// An operator-defined sub-category of a product module.
    /// e.g. Under SavingProduct: "Compulsory Savings", "Voluntary Savings", "Junior Savings"
    /// e.g. Under LoanProduct: "Personal Loan", "Group Loan", "Business Loan", "Mortgage"
    ///
    /// ProductModuleType identifies which of the five modules this type belongs to.
    /// Series is used for document numbering within the type.
    /// </summary>
    public class ProductType : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public int Series { get; set; }
        public ProductModuleType Module { get; set; }
        public bool IsSystem { get; set; }   // system types cannot be deleted
        public bool IsActive { get; set; } = true;
        public string? Description { get; set; }

        public ICollection<SavingProduct> SavingProducts { get; set; } = [];
        public ICollection<LoanProduct> LoanProducts { get; set; } = [];
        public ICollection<ShareProduct> ShareProducts { get; set; } = [];
        public ICollection<TimedepositProduct> TimedepositProducts { get; set; } = [];
        public ICollection<InsuranceProduct> InsuranceProducts { get; set; } = [];

    }

}
