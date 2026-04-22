using Argent.Api.Domain.Enums;

namespace Argent.Api.Infrastructure.Core.Modules.Kyc.DataObjects {
    /// <summary>
    /// Class represents summery of liet view for all types
    /// </summary>
    public class CustomerSummaryDto {
        public long Id { get; set; }
        public string ClientCode { get; set; } = string.Empty;
        public CustomerType CustomerType { get; set; }
        /// <summary>
        /// Display name displayed as FirstName MiddleName LastName, RegisteredName, LegalName
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;   
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string BranchName { get; set; } = string.Empty;
        public bool Active { get; set; }
        public bool Approved { get; set; }
        public bool Exited { get; set; }
        public bool CanTransact { get; set; }
        public DateTime RegisteredOn { get; set; }
    }
}
