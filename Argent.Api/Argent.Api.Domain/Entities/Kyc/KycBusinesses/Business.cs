using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Support.KycLookup;

namespace Argent.Api.Domain.Entities.Kyc.KycBusinesses {
    /// <summary>
    /// A business entity customer (company, NGO, cooperative, etc.)
    /// </summary>
    public class Business : CustomerBase {
        [Encryptable("Legal Name")]
        public string LegalName { get; set; } = string.Empty;

        public long? BusinessFilter1Id { get; set; }
        public CustomerFilter? BusinessFilter1 { get; set; }

        public long? BusinessFilter2Id { get; set; }
        public CustomerFilter? BusinessFilter2 { get; set; }

        public ICollection<Signatory> Signatories { get; set; } = [];
    }
}
