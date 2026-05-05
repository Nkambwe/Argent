using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Accounting.Cashflow {
    /// <summary>
    /// A cheque book assigned to a BankAccount, covering a range of cheque numbers.
    /// </summary>
    public class ChequeBook : BaseEntity {
        public long BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; } = null!;

        public string SerialNumber { get; set; } = string.Empty;
        public string FirstChequeNumber { get; set; } = string.Empty;
        public string LastChequeNumber { get; set; } = string.Empty;
        public int NumberOfLeafs { get; set; }
        public string? LastIssuedCheque { get; set; }
        public BookStatus Status { get; set; } = BookStatus.Active;

        public ICollection<Cheque> Cheques { get; set; } = [];
    }
}
