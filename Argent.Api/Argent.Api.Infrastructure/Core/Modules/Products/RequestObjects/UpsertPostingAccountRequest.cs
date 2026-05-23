using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Products.RequestObjects {
    public class UpsertPostingAccountRequest {
        public PostingPurpose PostingPurpose { get; set; }
        public CustomerSegment CustomerSegment { get; set; }
        public string LedgerNumber { get; set; } = string.Empty;
        public string? CostCentreCode { get; set; }
        public string? RevenueCentreCode { get; set; }
    }
}
