using Argent.Api.Domain.Common;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Kyc {
    /// <summary>
    /// Employment history with employer details.
    /// </summary>
    public class EmploymentHistory : BaseEntity {
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public string Employer { get; set; } = string.Empty;
        public string? PositionHeld { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public bool IsCurrentEmployer { get; set; }
        public decimal Earning { get; set; }
        public string? Notes { get; set; }
    }

}
