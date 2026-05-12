namespace Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects {
    public class UpdateLedgerHeaderRequest {
        public string LedgerName { get; set; } = string.Empty;
        public string? ParentHeader { get; set; }
        public long GroupIndex { get; set; }
        public long LedgerIndex { get; set; }
    }

}
