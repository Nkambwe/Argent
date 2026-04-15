using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Support.KycLookup {
    public class Village : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Parish { get; set; }
        public string? SubCounty { get; set; }
        public string? County { get; set; }
        public string? District { get; set; }
        public string? Region { get; set; }
    }
}
