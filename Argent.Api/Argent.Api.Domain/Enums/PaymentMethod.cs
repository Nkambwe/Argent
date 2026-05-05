
namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// How a cash or voucher transaction was settled.
    /// </summary>
    public enum PaymentMethod {
        Cash = 1,
        Cheque = 2,
        BankTransfer = 3,
        Card = 4,
        MobileMoney = 5,
        Other = 99
    }
}
