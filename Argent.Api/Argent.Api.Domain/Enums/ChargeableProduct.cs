namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// Which product module a charge applies to.
    /// Used instead of scattered nullable FKs to every product entity.
    /// </summary>
    public enum ChargeableProduct {
        None = 0,
        Savings = 1,
        TimeDeposit = 2,
        Shares = 3,
        Loan = 4,
        Insurance = 5
    }

}
