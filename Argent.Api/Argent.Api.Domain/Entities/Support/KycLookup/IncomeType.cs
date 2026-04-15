using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Support.KycLookup {
    public class IncomeType : BaseEntity {
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Salary, Business, Agriculture, etc. 
        /// </summary>
        public string Name { get; set; } = string.Empty;   
        public string? Notes { get; set; }
    }
}
