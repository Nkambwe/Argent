namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// Interest withdraw option for timedeposit accounts
    /// </summary>
    public enum InterestWithdrawMode {
        Maturity = 1,
        Monthly = 2,
        BiMonthly = 3,
        Quarterly = 4,
        SemiAnnually = 5
    }
}
