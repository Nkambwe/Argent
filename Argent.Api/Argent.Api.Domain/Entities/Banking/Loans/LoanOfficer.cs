using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Access;

namespace Argent.Api.Domain.Entities.Banking.Loans {
    public class LoanOfficer: BaseEntity {
        public string LoanOfficerCode { get; set; } = string.Empty;
        public decimal ApprovalLimit { get; set; } = decimal.Zero;
        public long AppUserId { get; set; }
        public virtual AppUser? AppUser { get; set; }
        public virtual ICollection<LoanOfficerLedgerAccount> LoanOfficerLedgerAccounts { get; set; } = [];
    }
}
