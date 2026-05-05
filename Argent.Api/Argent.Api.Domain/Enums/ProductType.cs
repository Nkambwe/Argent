namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// Which product module a charge applies to.
    /// Used instead of scattered nullable FKs to every product entity.
    /// </summary>
    public enum ProductType {
        Savings = 1,
        TimeDeposit = 2,
        Shares = 3,
        Insurance = 4,
        Loan = 5,
        Registration = 6
    }

}
