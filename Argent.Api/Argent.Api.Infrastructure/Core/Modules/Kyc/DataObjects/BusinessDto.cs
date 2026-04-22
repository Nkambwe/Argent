namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class BusinessDto {
        public long Id { get; set; }
        public string ClientCode { get; set; } = string.Empty;
        public string LegalName { get; set; } = string.Empty;
        public string? PermanentAddress { get; set; }
        public string? PrimaryLine { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? Notes { get; set; }
        public long BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public bool Active { get; set; }
        public bool Approved { get; set; }
        public bool Exited { get; set; }
        public bool CanTransact { get; set; }
        public DateTime RegisteredOn { get; set; }
        public int SignatoryCount { get; set; }
        public string? BusinessFilter1 { get; set; }
        public string? BusinessFilter2 { get; set; }
        public IEnumerable<ContactDto> Contacts { get; set; } = [];
        public IEnumerable<SignatoryDto> Signatories { get; set; } = [];
    }
}
