namespace Argent.Api.Infrastructure.Core.Modules.Access.RequestObjects {
    public class SetPolicyOverrideRequest {
        public long SystemPolicyId { get; set; }
        public string OverrideValue { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }


}
