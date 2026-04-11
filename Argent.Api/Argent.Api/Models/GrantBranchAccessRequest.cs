namespace Argent.Api.Models {
    public class GrantBranchAccessRequest {
        public long BranchId { get; set; }
        public bool CanPost { get; set; } = true;
    }
}
