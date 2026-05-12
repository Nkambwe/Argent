
using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects {
    public class UpdateLedgerAccountRequest {
        public string LedgerName { get; set; } = string.Empty;
        public long LedgerAccountHeaderId { get; set; }
        public NormalBalance NormalBalance { get; set; }
        public PostingType PostingType { get; set; }
        public bool AllowManualPosting { get; set; }
        public bool ShowParticulars { get; set; }
        public string? Notes { get; set; }
        public long GroupIndex { get; set; }
        public long LedgerIndex { get; set; }
    }

}
