
namespace Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects {
    public class LedgerAccountDto {
        public long Id { get; set; }
        public long LedgerAccountHeaderId { get; set; }
        public string HeaderName { get; set; } = string.Empty;
        public long AccountsChartId { get; set; }
        public string LedgerNumber { get; set; } = string.Empty;
        public string LedgerName { get; set; } = string.Empty;
        public string AccountClassification { get; set; } = string.Empty;
        public string AccountNature { get; set; } = string.Empty;
        public string NormalBalance { get; set; } = string.Empty;
        public string PostingType { get; set; } = string.Empty;
        public bool AllowManualPosting { get; set; }
        public bool ShowParticulars { get; set; }
        public bool Suspended { get; set; }
        public decimal Balance { get; set; }
        public string? Notes { get; set; }
        public long GroupIndex { get; set; }
        public long LedgerIndex { get; set; }
    }

}
