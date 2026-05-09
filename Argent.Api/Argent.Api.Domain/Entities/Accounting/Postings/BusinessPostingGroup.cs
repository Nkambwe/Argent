using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting.Postings {
    /// <summary>
    /// Business posting group — the entity type perspective (Customer, Supplier, Staff, etc.).
    /// Controls receivable/payable GL accounts based on who the transaction is with.
    /// </summary>
    public class BusinessPostingGroup : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public BusinessType BusinessType { get; set; }
        public bool IsActive { get; set; } = true;
        public string Notes { get; set; } = string.Empty;

        /*Business GL account mappings*/
        public string ReceivablesAccountNumber { get; set; } = string.Empty;
        public string PayablesAccountNumber { get; set; } = string.Empty;
        public string PrepaymentAccountNumber { get; set; } = string.Empty;
    }
}
