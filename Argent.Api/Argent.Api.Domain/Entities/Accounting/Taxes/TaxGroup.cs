using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Journals;
using Argent.Api.Domain.Entities.Products;

namespace Argent.Api.Domain.Entities.Accounting.Taxes {
    /// <summary>
    /// Class represents tax group eg. VAT, Income Tax,Withholding tax etc.
    /// </summary>
    public class TaxGroup : BaseEntity {
        public string SerieIdentifier { get; set; }= string.Empty;
        public string SeriePrefix { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int LastSeries { get; set; }
        public string Notes { get; set; } = string.Empty;
        public virtual ICollection<TimedepositProductTaxGroup> TimedepositProducts { get; set; } = [];
        public virtual ICollection<InsuranceProductTaxGroup> InsuranceProducts { get; set; } = [];
        public virtual ICollection<ShareProductTaxGroup> ShareProducts { get; set; } = [];
        public virtual ICollection<LoanProductTaxGroup> LoanProducts { get; set; } = [];
        public virtual ICollection<SavingProductTaxGroup> SavingProducts { get; set; } = [];
        public virtual ICollection<JournalTypeTaxGroup> JournalTypes { get; set; } = [];
        public virtual ICollection<Tax> Taxes { get; set; } = [];
    }

}
