namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    public class IndividualDto {
        public long Id { get; set; }
        public string ClientCode { get; set; } = string.Empty;
        public string? Statistic { get; set; }
        public string? Reference { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = string.Empty;
        public string FullName => !string.IsNullOrWhiteSpace(MiddleName) ? $"{FirstName} {MiddleName} {LastName}".Replace("  ", " ").Trim() : $"{FirstName} {LastName}".Replace("  ", " ").Trim();
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
        public string? Photo { get; set; }
        public string? Signature { get; set; }
        public string? PermanentAddress { get; set; }
        public string? MailAddress { get; set; }
        public string? PrimaryLine { get; set; }
        public string? SecondaryLine { get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? City { get; set; }
        public string? Town { get; set; }
        public string? WhatsApp { get; set; }
        public string? Facebook { get; set; }
        public string? Instagram { get; set; }
        public string? Twitter { get; set; }
        public string? Notes { get; set; }
        public long BranchId { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Nationality { get; set; }
        public string? Profession { get; set; }
        public string? Education { get; set; }
        public string? Village { get; set; }
        public string? Filter1 { get; set; }
        public string? Filter2 { get; set; }
        public string? Filter3 { get; set; }
        public bool Active { get; set; }
        public bool Approved { get; set; }
        public DateTime? ApprovedOn { get; set; }
        public string? ApprovedBy { get; set; }
        public bool Exited { get; set; }
        public DateTime? ExitedOn { get; set; }
        public bool CanTransact { get; set; }
        public bool HoldShares { get; set; }
        public DateTime RegisteredOn { get; set; }
        public IEnumerable<ContactDto> Contacts { get; set; } = [];
    }
}
