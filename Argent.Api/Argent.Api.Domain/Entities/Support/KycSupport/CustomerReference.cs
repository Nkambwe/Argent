using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Support.KycSupport {
    /// <summary>
    /// Record handles PersonId?/MemberId?/GroupId?/BusinessId? nullable FK pattern.
    /// EF maps this as two columns: customer_id and customer_type.
    /// Only one customer type should be set per record.
    /// </summary>
    public record CustomerReference(long CustomerId, CustomerType CustomerType);

}
