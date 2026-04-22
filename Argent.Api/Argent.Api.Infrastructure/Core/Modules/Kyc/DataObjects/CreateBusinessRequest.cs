namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class CreateBusinessRequest {
        public string LegalName { get; set; } = string.Empty;
        public long BranchId { get; set; }
        public string? PrimaryLine { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? PermanentAddress { get; set; }
        public string? MailAddress { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? WhatsApp { get; set; }
        public string? Notes { get; set; }
        public string? Statistic { get; set; }
        public string? Reference { get; set; }
        public long? VillageId { get; set; }
        public long? Filter1Id { get; set; }
        public long? Filter2Id { get; set; }
        public long? Filter3Id { get; set; }
        public long? BusinessFilter1Id { get; set; }
        public long? BusinessFilter2Id { get; set; }
        public List<CreateContactRequest> Contacts { get; set; } = [];
        public List<CreateSignatoryRequest> Signatories { get; set; } = [];
    }
}
