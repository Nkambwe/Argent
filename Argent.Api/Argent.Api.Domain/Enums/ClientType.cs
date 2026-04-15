
namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// Broad classification of client: retail individual, group-based, corporate, etc.
    /// Drives which sub-type screens and workflows are shown.
    /// </summary>
    public enum ClientType {
        Individual = 1,
        GroupMember = 2,
        Group = 3,
        Business = 4,
        Guarantor = 5
    }

}
