using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// Top-level bank group — e.g. "Standard Chartered Bank".
    /// A bank has one or more branches (BankBranch), each with accounts (BankAccount).
    /// </summary>
    public class Bank : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Contact { get; set; }
        public string? Telephone { get; set; }
        public string? Email { get; set; }
        public string? Fax { get; set; }

        public long? IbanId { get; set; }
        public Iban? Iban { get; set; }

        public long? SwiftId { get; set; }
        public Swift? Swift { get; set; }

        public ICollection<BankBranch> Branches { get; set; } = [];
    }
}
