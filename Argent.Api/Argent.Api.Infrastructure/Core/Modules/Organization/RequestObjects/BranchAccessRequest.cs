

namespace Argent.Api.Infrastructure.Core.Modules.Organization.RequestObjects {
    public class BranchAccessRequest {
        public long BranchId { get; set; }
        public string BranchCode { get; set; } = string.Empty;
        public bool CanPost { get; set; } = true;
    }
}
