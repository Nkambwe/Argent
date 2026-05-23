using Argent.Api.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Argent.Api.Infrastructure.Core.Modules.Products.OverrideObjects {
    /// <summary>
    /// Allows overriding specific configuration values at product creation.
    /// Unset properties inherit module defaults.
    /// </summary>
    public class SavingProductConfigOverrides {
        public bool? InterestBasedProduct { get; set; }
        public decimal? InterestRate { get; set; }
        public SavingInterestCalculation? InterestMethod { get; set; }
        public bool? TurnOnOverdraftProtection { get; set; }
        public decimal? OverdraftInterestRate { get; set; }
        public int? OverdraftPeriod { get; set; }
        public int? ConsiderDormantAfterDaysOfInactivity { get; set; }
        public bool? BookSavingsToGeneralLedger { get; set; }
        public int? MinimumClientAge { get; set; }
        public decimal? MinimumBalanceIndividualAccounts { get; set; }
        public decimal? MinimumBalanceGroupAccounts { get; set; }
        public decimal? MinimumBalanceBusinessAccounts { get; set; }
    }
}
