
namespace Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects {
    public class LedgerAccountTreeDto {
        public LedgerAccountHeaderDto Header { get; set; } = null!;
        public IEnumerable<LedgerAccountTreeDto> Children { get; set; } = [];
        public IEnumerable<LedgerAccountDto> Accounts { get; set; } = [];
        public IEnumerable<LedgerAccountTotalDto> Totals { get; set; } = [];
    }

}
