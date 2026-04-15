
namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// Discriminates the type of customer record across the system.
    /// Used on support entities (Exit, Blacklist, etc.) to replace the
    /// scattered nullable PersonId/MemberId/GroupId/BusinessId FK pattern.
    /// </summary>
    public enum CustomerType {
        Individual = 1,
        Member = 2,
        Group = 3,
        Business = 4
    }

}
