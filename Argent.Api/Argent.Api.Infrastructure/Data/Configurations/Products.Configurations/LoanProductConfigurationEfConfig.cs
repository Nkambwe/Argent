using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class LoanProductConfigurationEfConfig : IEntityTypeConfiguration<LoanProductConfiguration> {
        public void Configure(EntityTypeBuilder<LoanProductConfiguration> b) {
            b.ToTable("prd_loan_configs");
            b.HasKey(x => x.Id);
            b.Property(x => x.LinkedSavingsProduct).HasMaxLength(20);
            b.Property(x => x.AutomaticRepaymentSavingProduct).HasMaxLength(20);
            b.Property(x => x.GuaranteeShareProduct).HasMaxLength(20);
            b.Property(x => x.GuaranteeSavingsProduct).HasMaxLength(20);
            b.Property(x => x.SmsSendingTime).HasMaxLength(10);

            // Decimal columns
            foreach (var col in new[]
            {
                nameof(LoanProductConfiguration.DefaultLoanAmountForPersonalLoans),
                nameof(LoanProductConfiguration.MinimumLoanAmountForPersonalLoans),
                nameof(LoanProductConfiguration.MaximumLoanAmountForPersonalLoans),
                nameof(LoanProductConfiguration.DefaultLoanAmountForGroupLoans),
                nameof(LoanProductConfiguration.MinimumLoanAmountForGroupLoans),
                nameof(LoanProductConfiguration.MaximumLoanAmountForGroupLoans),
                nameof(LoanProductConfiguration.DefaultLoanAmountForGroupMembers),
                nameof(LoanProductConfiguration.DefaultLoanAmountForBusinessLoans),
                nameof(LoanProductConfiguration.MinimumLoanAmountForBusinessLoans),
                nameof(LoanProductConfiguration.MaximumLoanAmountForBusinessLoans),
                nameof(LoanProductConfiguration.IncomePercentage),
                nameof(LoanProductConfiguration.CollateralPercentageForPersonalLoans),
                nameof(LoanProductConfiguration.CollateralPercentageForGroupLoans),
                nameof(LoanProductConfiguration.CollateralPercentageForBusinessLoans),
                nameof(LoanProductConfiguration.ShareGuaranteePercentageForPersonalLoans),
                nameof(LoanProductConfiguration.ShareGuaranteePercentageForGroupLoans),
                nameof(LoanProductConfiguration.ShareGuaranteePercentageForBusinessLoans),
                nameof(LoanProductConfiguration.SavingsGuaranteePercentageForPersonalLoans),
                nameof(LoanProductConfiguration.SavingsGuaranteePercentageForGroupLoans),
                nameof(LoanProductConfiguration.SavingsGuaranteePercentageForBusinessLoans),
                nameof(LoanProductConfiguration.AcceptableCreditRiskForSavingsGuaranteedLoans),
                nameof(LoanProductConfiguration.MinimumAmountChargedAsPenalty),
                nameof(LoanProductConfiguration.DefaultInterestRateForPersonalLoans),
                nameof(LoanProductConfiguration.DefaultInterestRateForGroupLoans),
                nameof(LoanProductConfiguration.DefaultInterestRateForBusinessLoans)
            })
            b.Property(col).HasColumnType("decimal(18,4)");
        }
    }

}
