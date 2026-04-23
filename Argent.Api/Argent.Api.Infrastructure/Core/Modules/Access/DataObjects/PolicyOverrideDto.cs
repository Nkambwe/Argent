namespace Argent.Api.Infrastructure.Core.Modules.Access.DataObjects {
    public class PolicyOverrideDto {
        public long Id { get; set; }
        public long SystemPolicyId { get; set; }
        public string PolicyName { get; set; } = string.Empty;
        public string PolicyDescription { get; set; } = string.Empty;
        public string OverrideValue { get; set; } = string.Empty;
        public string? Reason { get; set; }
    }

}
