namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class RejectCustomerRequest {
        public long ReasonId { get; set; }
        public string? Notes { get; set; }
    }
}
