
using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects {
    public class CreateLedgerAccountRequest {
        public long LedgerAccountHeaderId { get; set; }
        public long AccountsChartId { get; set; }
        public string LedgerNumber { get; set; } = string.Empty;
        public string LedgerName { get; set; } = string.Empty;
        public AccountClassification AccountClassification { get; set; }
        public AccountNature AccountNature { get; set; }
        public NormalBalance NormalBalance { get; set; }
        public PostingType PostingType { get; set; }
        public bool AllowManualPosting { get; set; } = true;
        public bool ShowParticulars { get; set; } = true;
        public string? Notes { get; set; }
        public long GroupIndex { get; set; }
        public long LedgerIndex { get; set; }
    }

}
