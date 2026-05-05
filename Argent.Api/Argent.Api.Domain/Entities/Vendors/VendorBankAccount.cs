using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;

namespace Argent.Api.Domain.Entities.Vendors {
    public class VendorBankAccount: BaseEntity {
        public long VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }
        public long BankAccountId { get; set; }
        public virtual BankAccount? BankAccount { get; set; }
        
    }
}
