namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// Which customer segments this product is available to.
    /// Used on loan products to restrict availability.
    /// </summary>
    public enum CustomerTarget {
        All = 0,
        Individual = 1,
        Group = 2,
        Business = 3
    }
}
