using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Vendors {
    public class PaymentDefault : BaseEntity {
        public PaymentMethod PaymentMethod { get; set; }
        
        public long? PaymentTermId { get; set; }
        public virtual PaymentTerm? PaymentTerm { get; set; }

        public long? VendorId { get; set; }
        public virtual Vendor? Vendor { get; set; }

        public long? BankAccountId { get; set; }
        public virtual BankAccount? BankAccount { get; set; }
    }

}
