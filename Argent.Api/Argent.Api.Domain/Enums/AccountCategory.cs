namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// Hierarchical category of a row in the chart of accounts.
    /// Drives presentation order and totalling in financial statements.
    /// </summary>
    public enum AccountCategory {
        /// <summary>
        /// Top-level section label, Balance Sheet, P&L
        /// </summary>
        None = 0,   
        /// <summary>
        /// Major category - Assets, Liabilities, Equity, Revenue, Expenses 
        /// </summary>
        Category = 1,   
        /// <summary>
        /// Sub-section Fixed Assets, Current Assets, etc. 
        /// </summary>
        SubCategory = 2,   
        /// <summary>
        /// Account grouping within a sub-section 
        /// </summary>
        Header = 3,   
        /// <summary>
        /// Postable leaf account, the only type that accepts transactions 
        /// </summary>
        Ledger = 4,   
        /// <summary>
        /// Sum of all Ledger accounts under a Header 
        /// </summary>
        HeaderTotal = 5,   
        /// <summary>
        /// Sum of all Header totals under a SubCategory 
        /// </summary>
        SubTotal = 6,   
        /// <summary>
        /// Sum of all SubTotals under a Category 
        /// </summary>
        CategoryTotal = 7,  
        /// <summary>
        /// Overall total - Total Assets, Total Equity and Liabilities, etc.
        /// </summary>
        GrandTotal = 8    
    }
}
