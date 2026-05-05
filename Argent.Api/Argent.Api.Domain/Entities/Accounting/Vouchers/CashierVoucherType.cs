using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Accounting.Cashflow;

namespace Argent.Api.Domain.Entities.Accounting.Vouchers {
    /// <summary>
    /// Cashiers authorized to create vouchers of a specific type.
    /// </summary>
    public class CashierVoucherType : BaseEntity {
        public long CashierId { get; set; }
        public Cashier Cashier { get; set; } = null!;
        public long VoucherTypeId { get; set; }
        public VoucherType VoucherType { get; set; } = null!;
    }
}
