using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    /// <summary>
    /// A named collection of charges that a product can reference.
    /// Products (Savings, Loans, Shares, etc.) link to a ChargeGroup
    /// to inherit all its associated charge items.
    ///
    /// Series tracking: the group maintains its own document numbering
    /// for charge transaction documents.
    /// </summary>
    public class ChargeGroup : BaseEntity {
        public string SeriesIdentifier { get; set; } = string.Empty;  // e.g. "CHG"
        public string SeriesPrefix { get; set; } = string.Empty;       // e.g. "CHG-"
        public string GroupName { get; set; } = string.Empty;
        public int LastSeries { get; set; }
        public string? Notes { get; set; }

        public ICollection<ChargeGroupItem> Items { get; set; } = [];
        public ICollection<SavingProduct> SavingProducts { get; set; } = [];
        public ICollection<LoanProduct> LoanProducts { get; set; } = [];
        public ICollection<ShareProduct> ShareProducts { get; set; } = [];
        public ICollection<TimedepositProduct> TimedepositProducts { get; set; } = [];
        public ICollection<InsuranceProduct> InsuranceProducts { get; set; } = [];
    }

}
