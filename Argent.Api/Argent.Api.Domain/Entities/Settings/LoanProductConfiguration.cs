using Argent.Api.Domain.Common;
using Argent.Api.Domain.Entities.Products;
using Argent.Api.Domain.Enums;

namespace Argent.Api.Domain.Entities.Settings {
    /// <summary>
    /// Typed configuration for a LoanProduct.
    /// Seeded from defaults when the product is created.
    ///
    /// Organised into logical sections:
    ///   - General: eligibility, approval stages, recalculation
    ///   - Per-segment: individual / group / business loan rules
    ///   - Auto repayment: savings-based repayment
    ///   - Penalty: calculation method and automation
    ///   - Collateral: requirements per segment
    ///   - Guarantee by shares / savings
    ///   - SMS reminders: pre/post due date notifications
    ///
    /// GL posting accounts are stored in ProductPostingAccount, not here.
    /// </summary>
    public class LoanProductConfiguration : BaseEntity {
        public long LoanProductId { get; set; }
        public LoanProduct LoanProduct { get; set; } = null!;

        #region General

        [ConfigurationParam("AvailableToSavingCustomersOnly", "Whether product is only available to saving clients", "bool")]
        public bool AvailableToSavingCustomersOnly { get; set; }

        [ConfigurationParam("LinkedSavingsProduct", "Code of savings product linked to this loan product", "string")]
        public string LinkedSavingsProduct { get; set; } = string.Empty;

        [ConfigurationParam("InterestDays", "Number of interest days in a year", "int")]
        public int InterestDays { get; set; } = 365;

        [ConfigurationParam("InterestWeeks", "Number of interest weeks in a year", "int")]
        public int InterestWeeks { get; set; } = 52;

        [ConfigurationParam("LoanApprovalStages", "Number of approval tiers required", "int")]
        public TierApproval LoanApprovalStages { get; set; } = TierApproval.Tier1;

        [ConfigurationParam("ForceModificationOfDueDates", "Whether due date modification is enforced at disbursement", "bool")]
        public bool ForceModificationOfDueDates { get; set; }

        [ConfigurationParam("ModificationMethod", "Due dates modification method", "int")]
        public LoanDueModificationMethod ModificationMethod { get; set; } = LoanDueModificationMethod.None;

        [ConfigurationParam("UseEffectiveInterestRate", "Whether effective interest rate is used", "bool")]
        public bool UseEffectiveInterestRate { get; set; }

        [ConfigurationParam("MailMergeLoanRecords", "Whether loan records should be mail merged", "bool")]
        public bool MailMergeLoanRecords { get; set; }

        [ConfigurationParam("MailMergeOption", "Mail merge stage", "int")]
        public MailMergeOption MailMergeOption { get; set; } = MailMergeOption.None;

        [ConfigurationParam("ApplyRecalculateInterest", "Whether interest must be recalculated at repayment", "bool")]
        public bool ApplyRecalculateInterest { get; set; }

        [ConfigurationParam("RecalculateInterestOnlyIfNotInDays", "Whether recalculation applies only when interest is not in days", "bool")]
        public bool RecalculateInterestOnlyIfNotInDays { get; set; }

        [ConfigurationParam("RecalculationMethod", "Interest recalculation type", "int")]
        public InterestCalculation RecalculationMethod { get; set; } = InterestCalculation.None;

        [ConfigurationParam("NoInterestResetAtRecalculation", "Whether unpaid interest is preserved during recalculation", "bool")]
        public bool NoInterestResetAtRecalculation { get; set; }

        [ConfigurationParam("FreezeInterestWhenInArrears", "Whether interest is frozen after set arrear days", "bool")]
        public bool FreezeInterestWhenInArrears { get; set; }

        [ConfigurationParam("DaysToFreezeInterest", "Days in arrears before interest is frozen", "int")]
        public int DaysToFreezeInterest { get; set; }

        [ConfigurationParam("DeclassifyInterestInArrears", "Whether to declassify interest in arrears", "bool")]
        public bool DeclassifyInterestInArrears { get; set; }

        [ConfigurationParam("DeclassifyPrincipalInArrears", "Whether to declassify principal in arrears", "bool")]
        public bool DeclassifyPrincipalInArrears { get; set; }

        [ConfigurationParam("CompoundInterestAtRepayment", "Whether interest is compounded at repayment for declining balance loans", "bool")]
        public bool CompoundInterestAtRepayment { get; set; }

        [ConfigurationParam("IgnoreMaximumLimit", "Whether maximum loan limit can be overridden", "bool")]
        public bool IgnoreMaximumLimit { get; set; }

        [ConfigurationParam("AllowPartialDisbursements", "Whether partial disbursements are allowed", "bool")]
        public bool AllowPartialDisbursements { get; set; }

        [ConfigurationParam("UseRepaymentPriority", "Whether repayment priority order is enforced", "bool")]
        public bool UseRepaymentPriority { get; set; }

        [ConfigurationParam("RepaymentPriority", "Repayment priority order", "int")]
        public RepaymentPriority RepaymentPriority { get; set; } = RepaymentPriority.Undefined;

        [ConfigurationParam("DaysToArrearForRepaymentPriority", "Days in arrears before repayment priority kicks in", "int")]
        public int DaysToArrearForRepaymentPriority { get; set; }

        [ConfigurationParam("UseDuplum", "Whether Duplum rule applies", "bool")]
        public bool UseDuplum { get; set; }

        [ConfigurationParam("PushDuesFromToMonthEnd", "Whether dues from a certain day are pushed to month end", "bool")]
        public bool PushDuesFromToMonthEnd { get; set; }

        [ConfigurationParam("PushDuesFrom", "Day of month from which dues are pushed to month end", "int")]
        public int PushDuesFrom { get; set; } = 26;

        [ConfigurationParam("ChargeWithholdingTaxOnFees", "Whether withholding tax applies on professional fees", "bool")]
        public bool ChargeWithholdingTaxOnFees { get; set; }

        [ConfigurationParam("ChargeStampDuty", "Whether stamp duty applies", "bool")]
        public bool ChargeStampDuty { get; set; }

        #endregion

        #region Individual loan rules

        [ConfigurationParam("MinimumDaysAsClientPersonalLoans", "Minimum days as client for individual loans", "int")]
        public int MinimumDaysAsClientPersonalLoans { get; set; }

        [ConfigurationParam("DefaultLoanAmountForPersonalLoans", "Default loan amount for individual loans", "decimal")]
        public decimal DefaultLoanAmountForPersonalLoans { get; set; }

        [ConfigurationParam("EnforceDefaultLoanAmountForPersonalLoans", "Whether default amount is enforced for individual loans", "bool")]
        public bool EnforceDefaultLoanAmountForPersonalLoans { get; set; }

        [ConfigurationParam("MinimumLoanAmountForPersonalLoans", "Minimum loan amount for individual loans", "decimal")]
        public decimal MinimumLoanAmountForPersonalLoans { get; set; }

        [ConfigurationParam("MaximumLoanAmountForPersonalLoans", "Maximum loan amount for individual loans", "decimal")]
        public decimal MaximumLoanAmountForPersonalLoans { get; set; }

        [ConfigurationParam("CannotExceedIncomePercentage", "Whether loan cannot exceed a percentage of income", "bool")]
        public bool CannotExceedIncomePercentage { get; set; }

        [ConfigurationParam("IncomePercentage", "Percentage of income that loan amount cannot exceed", "decimal")]
        public decimal IncomePercentage { get; set; }

        [ConfigurationParam("DefaultInterestRateForPersonalLoans", "Default interest rate for individual loans", "decimal")]
        public decimal DefaultInterestRateForPersonalLoans { get; set; }

        [ConfigurationParam("EnforceDefaultInterestRateForPersonalLoans", "Whether default rate is enforced for individual loans", "bool")]
        public bool EnforceDefaultInterestRateForPersonalLoans { get; set; }

        [ConfigurationParam("MinimumMonthlyPeriodForPersonalLoans", "Minimum monthly period for individual loans", "int")]
        public int MinimumMonthlyPeriodForPersonalLoans { get; set; }

        [ConfigurationParam("MaximumMonthlyPeriodForPersonalLoans", "Maximum monthly period for individual loans", "int")]
        public int MaximumMonthlyPeriodForPersonalLoans { get; set; }

        [ConfigurationParam("DefaultGracePeriodForPersonalLoans", "Default grace period in days for individual loans", "int")]
        public int DefaultGracePeriodForPersonalLoans { get; set; }

        [ConfigurationParam("MaximumGracePeriodForPersonalLoans", "Maximum grace period in days for individual loans", "int")]
        public int MaximumGracePeriodForPersonalLoans { get; set; }

        [ConfigurationParam("DefaultInstallmentsForPersonalLoans", "Default installments for individual loans", "int")]
        public int DefaultInstallmentsForPersonalLoans { get; set; }

        [ConfigurationParam("DefaultInstallmentTypeForPersonalLoans", "Default installment type for individual loans", "int")]
        public InstallmentType DefaultInstallmentTypeForPersonalLoans { get; set; } = InstallmentType.Undefined;

        [ConfigurationParam("DefaultInterestCalculationForPersonalLoans", "Default interest calculation for individual loans", "int")]
        public InterestCalculation DefaultInterestCalculationForPersonalLoans { get; set; } = InterestCalculation.None;

        [ConfigurationParam("DeductInterestAtDisbursementForPersonalLoans", "Whether interest is deducted at disbursement for individual loans", "bool")]
        public bool DeductInterestAtDisbursementForPersonalLoans { get; set; }

        [ConfigurationParam("CalculateInterestInDaysForPersonalLoans", "Whether interest is calculated in days for individual loans", "bool")]
        public bool CalculateInterestInDaysForPersonalLoans { get; set; }

        [ConfigurationParam("CalculateInterestInGracePeriodForPersonalLoans", "Whether interest is calculated in grace period for individual loans", "bool")]
        public bool CalculateInterestInGracePeriodForPersonalLoans { get; set; }

        [ConfigurationParam("CompoundInterestOnGracePeriodForPersonalLoans", "Whether interest is compounded in grace period for individual loans", "bool")]
        public bool CompoundInterestOnGracePeriodForPersonalLoans { get; set; }

        [ConfigurationParam("SeparateInstallmentsInGracePeriodForPersonalLoans", "Whether grace period installments are separated for individual loans", "bool")]
        public bool SeparateInstallmentsInGracePeriodForPersonalLoans { get; set; }

        [ConfigurationParam("RequireInterestPaymentUpfrontForPersonalLoans", "Whether upfront interest payment is required for individual loans", "bool")]
        public bool RequireInterestPaymentUpfrontForPersonalLoans { get; set; }

        [ConfigurationParam("InstallmentCommissionForPersonalLoans", "Installment-based commission type for individual loans", "int")]
        public InstallmentBasedCommission InstallmentCommissionForPersonalLoans { get; set; } = InstallmentBasedCommission.None;

        [ConfigurationParam("CompoundInstallmentCommissionForPersonalLoans", "Whether installment commission is compounded for individual loans", "bool")]
        public bool CompoundInstallmentCommissionForPersonalLoans { get; set; }

        [ConfigurationParam("DaysToArrearPersonalLoans", "Days before individual loan is considered in arrears", "int")]
        public int DaysToArrearPersonalLoans { get; set; }

        #endregion

        #region Group loan rules

        [ConfigurationParam("MinimumDaysAsClientGroupLoans", "Minimum days as client for group loans", "int")]
        public int MinimumDaysAsClientGroupLoans { get; set; }

        [ConfigurationParam("MinimumDaysAsMember", "Minimum days as group member before loan", "int")]
        public int MinimumDaysAsMember { get; set; }

        [ConfigurationParam("DefaultLoanAmountForGroupLoans", "Default loan amount for group loans", "decimal")]
        public decimal DefaultLoanAmountForGroupLoans { get; set; }

        [ConfigurationParam("MinimumLoanAmountForGroupLoans", "Minimum loan amount for group loans", "decimal")]
        public decimal MinimumLoanAmountForGroupLoans { get; set; }

        [ConfigurationParam("MaximumLoanAmountForGroupLoans", "Maximum loan amount for group loans", "decimal")]
        public decimal MaximumLoanAmountForGroupLoans { get; set; }

        [ConfigurationParam("DefaultLoanAmountForGroupMembers", "Default loan amount per group member", "decimal")]
        public decimal DefaultLoanAmountForGroupMembers { get; set; }

        [ConfigurationParam("CheckSavingsGuaranteePerMember", "Whether savings guarantee is checked per member", "bool")]
        public bool CheckSavingsGuaranteePerMember { get; set; }

        [ConfigurationParam("DefaultLoanCycle", "Default loan cycle for group loans", "int")]
        public int DefaultLoanCycle { get; set; }

        [ConfigurationParam("DefaultInterestRateForGroupLoans", "Default interest rate for group loans", "decimal")]
        public decimal DefaultInterestRateForGroupLoans { get; set; }

        [ConfigurationParam("MinimumMonthlyPeriodForGroupLoans", "Minimum monthly period for group loans", "int")]
        public int MinimumMonthlyPeriodForGroupLoans { get; set; }

        [ConfigurationParam("MaximumMonthlyPeriodForGroupLoans", "Maximum monthly period for group loans", "int")]
        public int MaximumMonthlyPeriodForGroupLoans { get; set; }

        [ConfigurationParam("DefaultGracePeriodForGroupLoans", "Default grace period in days for group loans", "int")]
        public int DefaultGracePeriodForGroupLoans { get; set; }

        [ConfigurationParam("MaximumGracePeriodForGroupLoans", "Maximum grace period in days for group loans", "int")]
        public int MaximumGracePeriodForGroupLoans { get; set; }

        [ConfigurationParam("DefaultInstallmentsForGroupLoans", "Default installments for group loans", "int")]
        public int DefaultInstallmentsForGroupLoans { get; set; }

        [ConfigurationParam("DefaultInstallmentTypeForGroupLoans", "Default installment type for group loans", "int")]
        public InstallmentType DefaultInstallmentTypeForGroupLoans { get; set; } = InstallmentType.Undefined;

        [ConfigurationParam("DefaultInterestCalculationForGroupLoans", "Default interest calculation for group loans", "int")]
        public InterestCalculation DefaultInterestCalculationForGroupLoans { get; set; } = InterestCalculation.None;

        [ConfigurationParam("DeductInterestAtDisbursementForGroupLoans", "Whether interest is deducted at disbursement for group loans", "bool")]
        public bool DeductInterestAtDisbursementForGroupLoans { get; set; }

        [ConfigurationParam("CalculateInterestInDaysForGroupLoans", "Whether interest is calculated in days for group loans", "bool")]
        public bool CalculateInterestInDaysForGroupLoans { get; set; }

        [ConfigurationParam("CalculateInterestInGracePeriodForGroupLoans", "Whether interest is calculated in grace period for group loans", "bool")]
        public bool CalculateInterestInGracePeriodForGroupLoans { get; set; }

        [ConfigurationParam("DaysToArrearGroupLoans", "Days before group loan is considered in arrears", "int")]
        public int DaysToArrearGroupLoans { get; set; }

        #endregion

        #region Business loan rules

        [ConfigurationParam("MinimumDaysAsClientBusinessLoans", "Minimum days as client for business loans", "int")]
        public int MinimumDaysAsClientBusinessLoans { get; set; }

        [ConfigurationParam("DefaultLoanAmountForBusinessLoans", "Default loan amount for business loans", "decimal")]
        public decimal DefaultLoanAmountForBusinessLoans { get; set; }

        [ConfigurationParam("MinimumLoanAmountForBusinessLoans", "Minimum loan amount for business loans", "decimal")]
        public decimal MinimumLoanAmountForBusinessLoans { get; set; }

        [ConfigurationParam("MaximumLoanAmountForBusinessLoans", "Maximum loan amount for business loans", "decimal")]
        public decimal MaximumLoanAmountForBusinessLoans { get; set; }

        [ConfigurationParam("DefaultInterestRateForBusinessLoans", "Default interest rate for business loans", "decimal")]
        public decimal DefaultInterestRateForBusinessLoans { get; set; }

        [ConfigurationParam("MinimumMonthlyPeriodForBusinessLoans", "Minimum monthly period for business loans", "int")]
        public int MinimumMonthlyPeriodForBusinessLoans { get; set; }

        [ConfigurationParam("MaximumMonthlyPeriodForBusinessLoans", "Maximum monthly period for business loans", "int")]
        public int MaximumMonthlyPeriodForBusinessLoans { get; set; }

        [ConfigurationParam("DefaultGracePeriodForBusinessLoans", "Default grace period in days for business loans", "int")]
        public int DefaultGracePeriodForBusinessLoans { get; set; }

        [ConfigurationParam("DefaultInstallmentsForBusinessLoans", "Default installments for business loans", "int")]
        public int DefaultInstallmentsForBusinessLoans { get; set; }

        [ConfigurationParam("DefaultInstallmentTypeForBusinessLoans", "Default installment type for business loans", "int")]
        public InstallmentType DefaultInstallmentTypeForBusinessLoans { get; set; } = InstallmentType.Undefined;

        [ConfigurationParam("DefaultInterestCalculationForBusinessLoans", "Default interest calculation for business loans", "int")]
        public InterestCalculation DefaultInterestCalculationForBusinessLoans { get; set; } = InterestCalculation.None;

        [ConfigurationParam("DaysToArrearBusinessLoans", "Days before business loan is considered in arrears", "int")]
        public int DaysToArrearBusinessLoans { get; set; }

        #endregion

        #region Auto repayment from savings

        [ConfigurationParam("AutomaticallyRepayFromSavings", "Whether repayments are automatically drawn from savings", "bool")]
        public bool AutomaticallyRepayFromSavings { get; set; }

        [ConfigurationParam("AutomaticRepaymentSavingProduct", "Savings product used for automatic repayments", "string")]
        public string AutomaticRepaymentSavingProduct { get; set; } = string.Empty;

        [ConfigurationParam("IncludeMinimumAmountOnAutomaticRepayment", "Whether minimum balance is considered in auto repayments", "bool")]
        public bool IncludeMinimumAmountOnAutomaticRepayment { get; set; }

        [ConfigurationParam("MinimumArrearDaysToAutomaticRepayment", "Minimum arrear days before auto repayment starts", "int")]
        public int MinimumArrearDaysToAutomaticRepayment { get; set; }

        #endregion

        #region Penalty

        [ConfigurationParam("PenaltyCalculationType", "Penalty calculation type", "int")]
        public PenaltyCalculationType PenaltyCalculationType { get; set; } = PenaltyCalculationType.None;

        [ConfigurationParam("PenaltyCalculationMethod", "Penalty calculation method", "int")]
        public PenaltyCalculationMethod PenaltyCalculationMethod { get; set; } = PenaltyCalculationMethod.None;

        [ConfigurationParam("TurnOnTaskBasedPenaltyCalculation", "Whether penalty is calculated by scheduled task", "bool")]
        public bool TurnOnTaskBasedPenaltyCalculation { get; set; }

        [ConfigurationParam("TurnOnLoginPenaltyCalculation", "Whether penalty is calculated at login", "bool")]
        public bool TurnOnLoginPenaltyCalculation { get; set; }

        [ConfigurationParam("CalculatePenaltyPerInstallmentDue", "Whether penalty is calculated per overdue installment", "bool")]
        public bool CalculatePenaltyPerInstallmentDue { get; set; }

        [ConfigurationParam("TurnOnPenaltyOnHolidaysAndWeekends", "Whether penalty accrues on holidays and weekends", "bool")]
        public bool TurnOnPenaltyOnHolidaysAndWeekends { get; set; }

        [ConfigurationParam("MinimumAmountChargedAsPenalty", "Minimum penalty amount", "decimal")]
        public decimal MinimumAmountChargedAsPenalty { get; set; }

        [ConfigurationParam("CalculatePenaltyAfterExpiration", "Whether penalty accrues after loan expires", "bool")]
        public bool CalculatePenaltyAfterExpiration { get; set; }

        [ConfigurationParam("AutoCalculatePenaltyAfterGracePeriod", "Whether penalty is auto-calculated after grace period", "bool")]
        public bool AutoCalculatePenaltyAfterGracePeriod { get; set; }

        [ConfigurationParam("AutoPenaltyGracePeriod", "Grace period in days before auto penalty starts", "int")]
        public int AutoPenaltyGracePeriod { get; set; }

        [ConfigurationParam("CapitalizeInterestAndPenaltiesOnAutoPenalty", "Whether interest and penalties are capitalized on auto calculation", "bool")]
        public bool CapitalizeInterestAndPenaltiesOnAutoPenalty { get; set; }

        public DateTime LastPenaltyCalculationDate { get; set; }

        #endregion

        #region Collateral

        [ConfigurationParam("RequireCollateralForPersonalLoans", "Whether collateral is required for individual loans", "bool")]
        public bool RequireCollateralForPersonalLoans { get; set; }

        [ConfigurationParam("CollateralPercentageForPersonalLoans", "Collateral percentage required for individual loans", "decimal")]
        public decimal CollateralPercentageForPersonalLoans { get; set; }

        [ConfigurationParam("EnforceCollateral", "Collateral enforcement method", "int")]
        public EnforceCollateral EnforceCollateral { get; set; } = EnforceCollateral.Undefined;

        [ConfigurationParam("RequireCollateralForGroupLoans", "Whether collateral is required for group loans", "bool")]
        public bool RequireCollateralForGroupLoans { get; set; }

        [ConfigurationParam("CollateralPercentageForGroupLoans", "Collateral percentage required for group loans", "decimal")]
        public decimal CollateralPercentageForGroupLoans { get; set; }

        [ConfigurationParam("RequireCollateralForBusinessLoans", "Whether collateral is required for business loans", "bool")]
        public bool RequireCollateralForBusinessLoans { get; set; }

        [ConfigurationParam("CollateralPercentageForBusinessLoans", "Collateral percentage required for business loans", "decimal")]
        public decimal CollateralPercentageForBusinessLoans { get; set; }

        #endregion

        #region Guarantee by shares

        [ConfigurationParam("CanGuaranteeLoanByShares", "Whether loan can be guaranteed by shares", "bool")]
        public bool CanGuaranteeLoanByShares { get; set; }

        [ConfigurationParam("GuaranteeShareProduct", "Share product used to guarantee loan", "string")]
        public string GuaranteeShareProduct { get; set; } = string.Empty;

        [ConfigurationParam("ShareGuaranteePercentageForPersonalLoans", "Share guarantee percentage for individual loans", "decimal")]
        public decimal ShareGuaranteePercentageForPersonalLoans { get; set; }

        [ConfigurationParam("ShareGuaranteePercentageForGroupLoans", "Share guarantee percentage for group loans", "decimal")]
        public decimal ShareGuaranteePercentageForGroupLoans { get; set; }

        [ConfigurationParam("ShareGuaranteePercentageForBusinessLoans", "Share guarantee percentage for business loans", "decimal")]
        public decimal ShareGuaranteePercentageForBusinessLoans { get; set; }

        #endregion

        #region Guarantee by savings
        [ConfigurationParam("CanGuaranteeLoanBySavings", "Whether loan can be guaranteed by savings", "bool")]
        public bool CanGuaranteeLoanBySavings { get; set; }

        [ConfigurationParam("GuaranteeSavingsProduct", "Savings product used to guarantee loan", "string")]
        public string GuaranteeSavingsProduct { get; set; } = string.Empty;

        [ConfigurationParam("SavingsGuaranteeType", "Type of deposit for savings guarantee", "int")]
        public GuaranteeDepositType SavingsGuaranteeType { get; set; } = GuaranteeDepositType.Undefined;

        [ConfigurationParam("RequireGuaranteeDepositAtDisbursement", "Whether guarantee deposit must be paid at disbursement", "bool")]
        public bool RequireGuaranteeDepositAtDisbursement { get; set; }

        [ConfigurationParam("SavingsGuaranteePercentageForPersonalLoans", "Savings guarantee percentage for individual loans", "decimal")]
        public decimal SavingsGuaranteePercentageForPersonalLoans { get; set; }

        [ConfigurationParam("SavingsGuaranteePercentageForGroupLoans", "Savings guarantee percentage for group loans", "decimal")]
        public decimal SavingsGuaranteePercentageForGroupLoans { get; set; }

        [ConfigurationParam("SavingsGuaranteePercentageForBusinessLoans", "Savings guarantee percentage for business loans", "decimal")]
        public decimal SavingsGuaranteePercentageForBusinessLoans { get; set; }

        [ConfigurationParam("AcceptableCreditRiskForSavingsGuaranteedLoans", "Acceptable credit risk for savings guaranteed loans", "decimal")]
        public decimal AcceptableCreditRiskForSavingsGuaranteedLoans { get; set; }

        #endregion

        #region SMS reminders
        [ConfigurationParam("TurnOnSendSmsBeforeFirstDuedate", "Whether first pre-due SMS is enabled", "bool")]
        public bool TurnOnSendSmsBeforeFirstDuedate { get; set; }

        [ConfigurationParam("DaysToSendSmsBeforeFirstDuedate", "Days before due date to send first SMS", "int")]
        public int DaysToSendSmsBeforeFirstDuedate { get; set; }

        [ConfigurationParam("TurnOnSendSmsBeforeSecondDuedate", "Whether second pre-due SMS is enabled", "bool")]
        public bool TurnOnSendSmsBeforeSecondDuedate { get; set; }

        [ConfigurationParam("DaysToSendSmsBeforeSecondDuedate", "Days before due date to send second SMS", "int")]
        public int DaysToSendSmsBeforeSecondDuedate { get; set; }

        [ConfigurationParam("TurnOnSendSmsBeforeThirdDuedate", "Whether third pre-due SMS is enabled", "bool")]
        public bool TurnOnSendSmsBeforeThirdDuedate { get; set; }

        [ConfigurationParam("DaysToSendSmsBeforeThirdDuedate", "Days before due date to send third SMS", "int")]
        public int DaysToSendSmsBeforeThirdDuedate { get; set; }

        [ConfigurationParam("TurnOnSendSmsAfterFirstDuedate", "Whether first post-due SMS is enabled", "bool")]
        public bool TurnOnSendSmsAfterFirstDuedate { get; set; }

        [ConfigurationParam("DaysToSendSmsAfterFirstDuedate", "Days after due date to send first SMS", "int")]
        public int DaysToSendSmsAfterFirstDuedate { get; set; }

        [ConfigurationParam("TurnOnSendSmsAfterSecondDuedate", "Whether second post-due SMS is enabled", "bool")]
        public bool TurnOnSendSmsAfterSecondDuedate { get; set; }

        [ConfigurationParam("DaysToSendSmsAfterSecondDuedate", "Days after due date to send second SMS", "int")]
        public int DaysToSendSmsAfterSecondDuedate { get; set; }

        [ConfigurationParam("TurnOnSendSmsAfterThirdDuedate", "Whether third post-due SMS is enabled", "bool")]
        public bool TurnOnSendSmsAfterThirdDuedate { get; set; }

        [ConfigurationParam("DaysToSendSmsAfterThirdDuedate", "Days after due date to send third SMS", "int")]
        public int DaysToSendSmsAfterThirdDuedate { get; set; }

        [ConfigurationParam("GroupSmsSendingOption", "Group SMS sending option", "int")]
        public GroupSms GroupSmsSendingOption { get; set; } = GroupSms.All;

        [ConfigurationParam("SmsSendingTime", "SMS sending time (HH:mm)", "string")]
        public string SmsSendingTime { get; set; } = string.Empty;

        #endregion
    }

}
