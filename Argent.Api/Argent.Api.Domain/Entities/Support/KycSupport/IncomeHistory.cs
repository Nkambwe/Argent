using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Support.KycLookup;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Support.KycSupport {
    /// <summary>
    /// Income source history (what income type and how much).
    /// </summary>
    public class IncomeHistory : BaseEntity {
        public long CustomerId { get; set; }
        public CustomerType CustomerType { get; set; }
        public string? Employer { get; set; }
        public string? PositionHeld { get; set; }
        public DateTime HiredOn { get; set; }
        public decimal Salary { get; set; }
        public bool IsCurrent { get; set; }
        public DateTime? Ended { get; set; }
        public Guid IncomeTypeId { get; set; }
        public IncomeType IncomeType { get; set; } = null!;
    }

}
