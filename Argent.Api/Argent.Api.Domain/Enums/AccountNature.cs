namespace Argent.Api.Domain.Enums {
    public enum AccountNature {
        Real = 1,   // permanent accounts (balance sheet)
        Nominal = 2,   // temporary accounts (income statement, closed at year end)
        Personal = 3    // accounts for specific persons/entities
    }
}
