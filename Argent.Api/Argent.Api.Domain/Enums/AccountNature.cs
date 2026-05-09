namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// Accounting nature — whether this account belongs to the balance sheet or income statement.
    /// </summary>
    public enum AccountNature {
        /// <summary>
        /// Balance sheet account (Assets, Liabilities, Equity) — carries forward 
        /// </summary>
        Balance = 1, 
        /// <summary>
        /// Income statement account (Revenue, Expenses) — closed at year end 
        /// </summary>
        Income = 2   
    }
}
