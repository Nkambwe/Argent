namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class BlacklistCustomerRequest {
        public long ReasonId { get; set; }
        public string? Notes { get; set; }
    }
}
