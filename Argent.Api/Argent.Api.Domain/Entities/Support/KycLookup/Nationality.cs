using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Support.KycLookup {
    public class Nationality : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
