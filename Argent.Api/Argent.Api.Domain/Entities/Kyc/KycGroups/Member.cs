using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Banking.Savings;
using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Argent.Api.Domain.Entities.Support.KycLookup;
using Argent.Api.Domain.Entities.Support.KycSupport;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc.KycGroups {
    /// <summary>
    /// A customer who is a member of a Group.
    /// Has personal identity fields similar to Individual, plus group linkage.
    /// </summary>
    public class Member : CustomerBase {

        public long GroupId { get; set; }
        public Group Group { get; set; } = null!;

        /// <summary>
        /// Member's registration number within the group.
        /// </summary>
        public string? MemberNumber { get; set; }

        [Encryptable("First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Encryptable("Middle Name")]
        public string? MiddleName { get; set; }

        [Encryptable("Last Name")]
        public string LastName { get; set; } = string.Empty;

        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? BirthPlace { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }

        [Encryptable("Spouse Name")]
        public string? SpouseName { get; set; }

        public int Children { get; set; }
        public int Dependents { get; set; }

        [Encryptable("Mother's Name")]
        public string? Mother { get; set; }

        [Encryptable("Father's Name")]
        public string? Father { get; set; }

        public bool Literate { get; set; }

        public string? Photo { get; set; }
        public string? Signature { get; set; }
        public string? RightThumbPrint { get; set; }
        public string? LeftThumbPrint { get; set; }

        public DateTime JoinedOn { get; set; }

        public long? TitleId { get; set; }
        public Title? Title { get; set; }

        public long? NationalityId { get; set; }
        public Nationality? Nationality { get; set; }

        public long? ProfessionId { get; set; }
        public Profession? Profession { get; set; }

        public long? EducationId { get; set; }
        public Education? Education { get; set; }

        public long? MemberFilter1Id { get; set; }
        public CustomerFilter? MemberFilter1 { get; set; }

        public long? MemberFilter2Id { get; set; }
        public CustomerFilter? MemberFilter2 { get; set; }

        public ICollection<IncomeHistory> IncomeHistories { get; set; } = [];
        public ICollection<EmploymentHistory> EmploymentHistories { get; set; } = [];
        public ICollection<Identification> Identifications { get; set; } = [];
        public ICollection<MemberPosition> Positions { get; set; } = [];
        public ICollection<MemberTransfer> Transfers { get; set; } = [];
        public ICollection<SavingPartner> SavingPartners { get; set; } = [];
    }
}
