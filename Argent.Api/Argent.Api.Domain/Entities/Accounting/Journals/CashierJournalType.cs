using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;

namespace Argent.Api.Domain.Entities.Accounting.Journals {
    /// <summary>
    /// Cashiers authorized to create journals of a specific type.
    /// </summary>
    public class CashierJournalType : BaseEntity {
        public long CashierId { get; set; }
        public Cashier Cashier { get; set; } = null!;
        public long JournalTypeId { get; set; }
        public JournalType JournalType { get; set; } = null!;
    }
}
