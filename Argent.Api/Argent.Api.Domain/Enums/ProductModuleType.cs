namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// Identifies which operational module a product belongs to.
    /// Used internally to distinguish product types without dynamic dispatch.
    /// </summary>
    public enum ProductModuleType {
        Savings = 1,
        Loan = 2,
        Share = 3,
        TimeDeposit = 4,
        Insurance = 5
    }
}
