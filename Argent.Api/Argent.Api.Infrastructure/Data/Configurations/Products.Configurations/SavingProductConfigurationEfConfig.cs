using Argent.Api.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Products.Configurations {
    public class SavingProductConfigurationEfConfig : IEntityTypeConfiguration<SavingProductConfiguration> {
        public void Configure(EntityTypeBuilder<SavingProductConfiguration> b) {
            b.ToTable("prd_saving_configs");
            b.HasKey(x => x.Id);
            b.Property(x => x.InterestRate).HasColumnType("decimal(10,4)");
            b.Property(x => x.OverdraftInterestRate).HasColumnType("decimal(10,4)");
            b.Property(x => x.NegativeBalanceInterestRate).HasColumnType("decimal(10,4)");
            b.Property(x => x.MinimumInterestOnNegativeBalance).HasColumnType("decimal(18,2)");
            b.Property(x => x.ChequeBookCharge).HasColumnType("decimal(18,2)");
            b.Property(x => x.MaximumAmountPerSmsTransaction).HasColumnType("decimal(18,2)");
            b.Property(x => x.ElectronicCardWithdrawLimit).HasColumnType("decimal(18,2)");
            b.Property(x => x.ElectronicCardPurchaseLimit).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinimumBalanceIndividualAccounts).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinimumInterestEarningBalanceIndividualAccounts).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinimumBalanceGroupAccounts).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinimumInterestEarningBalanceGroupAccounts).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinimumBalanceBusinessAccounts).HasColumnType("decimal(18,2)");
            b.Property(x => x.MinimumInterestEarningBalanceBusinessAccounts).HasColumnType("decimal(18,2)");
        }
    }

}
