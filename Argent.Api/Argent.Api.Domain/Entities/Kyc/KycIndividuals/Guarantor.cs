using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Argent.Api.Domain.Entities.Support.KycLookup;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc.KycIndividuals {
    /// <summary>
    /// A loan guarantor. May or may not be an existing customer (IsClient flag).
    /// Deliberately thin — no account links, no filter classification.
    /// </summary>
    public class Guarantor : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string? Reference { get; set; }

        [Encryptable("First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Encryptable("Middle Name")]
        public string? MiddleName { get; set; }

        [Encryptable("Last Name")]
        public string LastName { get; set; } = string.Empty;

        public Gender Gender { get; set; }

        /// <summary>
        /// True if this guarantor is also a registered customer.
        /// </summary>
        public bool IsClient { get; set; }

        public string? Photo { get; set; }
        public string? Signature { get; set; }

        [Encryptable("Address")]
        public string? PermanentAddress { get; set; }

        [Encryptable("Phone")]
        public string? Telephone { get; set; }

        [Encryptable("Mobile")]
        public string? Mobile { get; set; }

        [Encryptable("Email")]
        public string? Email { get; set; }

        public string? City { get; set; }
        public string? Town { get; set; }
        public string? Notes { get; set; }

        public long? TitleId { get; set; }
        public Title? Title { get; set; }

        public long? NationalityId { get; set; }
        public Nationality? Nationality { get; set; }

        public long? VillageId { get; set; }
        public Village? Village { get; set; }

        public long? ProfessionId { get; set; }
        public Profession? Profession { get; set; }

        public ICollection<Identification> Identifications { get; set; } = [];
    }
}
