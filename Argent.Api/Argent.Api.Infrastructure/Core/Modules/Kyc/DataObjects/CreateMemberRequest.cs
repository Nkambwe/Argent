namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class CreateMemberRequest {
        public long GroupId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? PermanentAddress { get; set; }
        public string? Notes { get; set; }
        public long? TitleId { get; set; }
        public long? NationalityId { get; set; }
        public long? ProfessionId { get; set; }
        public long? EducationId { get; set; }
        public long? VillageId { get; set; }
        public long? Filter1Id { get; set; }
        public long? Filter2Id { get; set; }
        public long? Filter3Id { get; set; }
        public long? MemberFilter1Id { get; set; }
        public long? MemberFilter2Id { get; set; }
        public string? Photo { get; set; }
        public string? Signature { get; set; }
        public List<CreateContactRequest> Contacts { get; set; } = [];
    }
}
