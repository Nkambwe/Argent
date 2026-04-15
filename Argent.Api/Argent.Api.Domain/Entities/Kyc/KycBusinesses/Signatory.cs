using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Kyc.KycFiles;

namespace Argent.Api.Domain.Entities.Kyc.KycBusinesses {
    /// <summary>
    /// An authorized signatory for a Business customer.
    /// Controls who can authorize business transactions.
    /// </summary>
    public class Signatory : BaseEntity {
        public long BusinessId { get; set; }
        public Business Business { get; set; } = null!;

        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Photo { get; set; }
        public string? Signature { get; set; }
        public string? Telephone { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }

        /// <summary>
        /// When true, this signatory can authorize transactions alone (no co-signatory needed).
        /// </summary>
        public bool CanSignAlone { get; set; }

        /// <summary>
        /// When true, this signatory has been suspended and cannot authorize transactions.
        /// </summary>
        public bool Suspended { get; set; }

        public string? PassCode { get; set; }
        public string? Notes { get; set; }

        public ICollection<OtherFile> Files { get; set; } = [];
        public ICollection<Identification> Identifications { get; set; } = [];
    }
}
