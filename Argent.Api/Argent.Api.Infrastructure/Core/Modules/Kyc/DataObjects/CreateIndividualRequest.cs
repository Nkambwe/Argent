namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class CreateIndividualRequest {

        #region Require Fields
        public long BranchId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        #endregion

        #region Conditionally required driven by System Configuration
        public string? MiddleName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? BirthPlace { get; set; }
        public string? MaritalStatus { get; set; }
        public string? SpouseName { get; set; }
        public int Children { get; set; }
        public int Dependents { get; set; }
        public string? Mother { get; set; }
        public string? Father { get; set; }
        public bool Literate { get; set; }
        public string? PermanentAddress { get; set; }
        public string? MailAddress { get; set; }
        public string? PrimaryLine { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? WhatsApp { get; set; }
        public string? Notes { get; set; }
        public string? Statistic { get; set; }
        public string? Reference { get; set; }

        #endregion

        #region Classification
        public long? TitleId { get; set; }
        public long? NationalityId { get; set; }
        public long? ProfessionId { get; set; }
        public long? EducationId { get; set; }
        public long? VillageId { get; set; }
        public long? Filter1Id { get; set; }
        public long? Filter2Id { get; set; }
        public long? Filter3Id { get; set; }
        #endregion

        #region Photo, signature paths uploaded separately, URLs stored in these fields
        public string? Photo { get; set; }
        public string? Signature { get; set; }
        #endregion

        // Next of kin contacts
        public List<CreateContactRequest> Contacts { get; set; } = [];
    }
}
