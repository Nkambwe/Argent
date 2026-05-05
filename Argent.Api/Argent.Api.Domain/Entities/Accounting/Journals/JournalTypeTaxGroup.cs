using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Taxes;

namespace Argent.Api.Domain.Entities.Accounting.Journals {
    public class JournalTypeTaxGroup: BaseEntity {
        public long JournalTypeId { get; set; }
        public virtual JournalType? JournalType { get; set; }
        public long TaxGroupId { get; set; }
        public virtual TaxGroup? TaxGroup { get; set; }
    }
}
