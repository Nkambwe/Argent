
namespace Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects {
    public class LedgerAccountHeaderDto {
        public long Id { get; set; }
        public string LedgerNumber { get; set; } = string.Empty;
        public string LedgerName { get; set; } = string.Empty;
        public string? ParentHeader { get; set; }
        public string AccountClassification { get; set; } = string.Empty;
        public string AccountCategory { get; set; } = string.Empty;
        public string AccountNature { get; set; } = string.Empty;
        public long GroupIndex { get; set; }
        public long LedgerIndex { get; set; }
        public int ChildHeaderCount { get; set; }
        public int LedgerAccountCount { get; set; }
    }

}
