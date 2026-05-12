
namespace Argent.Api.Infrastructure.Core.Modules.Accounting.RequestObjects {
    public class LedgerSearchRequest {
        public string? Classification { get; set; }
        public string? Nature { get; set; }
        public bool? Suspended { get; set; }
        public long? HeaderId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
