namespace Argent.Api.Domain.Enums {
    /// <summary>
    /// The accounting purpose of a product posting account mapping.
    /// One row per (ProductId, PostingPurpose, CustomerSegment) combination.
    /// This replaces the 40+ string ledger fields in the original configuration classes.
    /// </summary>
    public enum PostingPurpose {
        //..savings
        Deposits = 100,
        InterestExpense = 101,
        AccruedInterestExpense = 102,
        AccruedInterestCost = 103,
        WithholdingTax = 104,
        OverdraftInterest = 105,
        ExpiredOverdraftInterest = 106,
        NegativeBalanceInterest = 107,
        StandingOrderHolding = 108,
        InterBranchTransfer = 109,
        ChequeBookSales = 110,

        //..loans
        PrincipalOutstanding = 200,
        LoanInterest = 201,
        AccruedLoanInterest = 202,
        InterestReceived = 203,
        LoanWriteOff = 204,
        ProvisionForBadDebt = 205,
        CostOnProvision = 206,
        AccruedPenalty = 207,
        AccruedCommission = 208,
        AccruedCharges = 209,
        RecoveryOfBadDebts = 210,
        LoanCheques = 211,
        CurrencyDifferences = 212,
        OverPayments = 213,
        Refinance = 214,
        StampDutyPrincipal = 215,
        StampDutyInterest = 216,
        LoanWithholdingTax = 217,

        //..shares
        ShareCapital = 300,
        DividendExpense = 301,
        AccruedDividend = 302,
        ShareRedemption = 303,
        ShareCheques = 304,
        ShareWithholdingTax = 305,

        //..time Deposits
        FixedDepositBalance = 400,
        FixedDepositInterest = 401,
        FixedDepositInterestDue = 402,
        FixedDepositPenalty = 403,
        TimedepositAccruedInterest = 404,
        TimedepositAccruedCost = 405,
        CashDifference = 406,
        TimedepositWithholdingTax = 407,

        //..insurance
        Claims = 500,
        AdministrationCost = 501,
        AdministrationFund = 502,
        InsuranceStampDuty = 503,
        InsuranceWithholdingTax = 504,

        //..shared
        OtherTax = 900
    }
}
