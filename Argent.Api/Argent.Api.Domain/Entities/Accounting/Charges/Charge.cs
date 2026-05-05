using Argent.Api.Domain.Common;

namespace Argent.Api.Domain.Entities.Accounting.Charges {
    /// <summary>
    /// A named charge definition that can apply to one or more product modules.
    /// Controls which business events trigger this charge (registration, savings,
    /// time deposits, shares, insurance, loans).
    ///
    /// A Charge is a template — ChargeItems carry the actual rate/amount logic.
    /// </summary>
    public class Charge : BaseEntity {
        public string Code { get; set; } = string.Empty;
        public string Series { get; set; } = string.Empty;
        public string ChargeName { get; set; } = string.Empty;
        public bool IsRated { get; set; }
        public bool AppliesToRegistration { get; set; }
        public bool AppliesToSavings { get; set; }
        public bool AppliesToTimeDeposits { get; set; }
        public bool AppliesToShares { get; set; }
        public bool AppliesToInsurance { get; set; }
        public bool AppliesToLoans { get; set; }

        public int LastCount { get; set; }
        public string? Notes { get; set; }

        public ICollection<ChargeItemCharge> ChargeItems { get; set; } = [];
    }

}
