
namespace Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects {
    public class LedgerAccountTotalDto {
        public long Id { get; set; }
        public string LedgerNumber { get; set; } = string.Empty;
        public string LedgerName { get; set; } = string.Empty;
        public string AccountCategory { get; set; } = string.Empty;
        public string TotalRange { get; set; } = string.Empty;
    }

}
