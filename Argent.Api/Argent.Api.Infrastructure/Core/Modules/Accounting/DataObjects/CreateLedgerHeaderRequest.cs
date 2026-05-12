
using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Accounting.DataObjects {
    public class CreateLedgerHeaderRequest {
        public string LedgerNumber { get; set; } = string.Empty;
        public string LedgerName { get; set; } = string.Empty;

        /// <summary>
        /// LedgerNumber of the parent header. Null = top-level.
        /// </summary>
        public string? ParentHeader { get; set; }

        /// <summary>
        /// Only SubCategory and Header are allowed via API.
        /// </summary>
        public AccountCategory AccountCategory { get; set; }

        public AccountClassification AccountClassification { get; set; }
        public AccountNature AccountNature { get; set; }
        public long GroupIndex { get; set; }
        public long LedgerIndex { get; set; }
    }

}
