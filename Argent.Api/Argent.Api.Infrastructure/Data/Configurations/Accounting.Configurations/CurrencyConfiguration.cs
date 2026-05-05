using Argent.Api.Domain.Entities.Accounting.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {

    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency> {
        public void Configure(EntityTypeBuilder<Currency> builder) {
            builder.ToTable("acc_currencies");
            builder.Property(c => c.Code).IsRequired().HasMaxLength(10);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(100);
            builder.Property(c => c.SmallUnit).HasMaxLength(50);
            builder.Property(c => c.Symbol).HasMaxLength(10);
            builder.Property(c => c.Country).HasMaxLength(100);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(c => c.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_currencies_code");

            builder.HasIndex(c => c.IsBaseCurrency)
                .HasDatabaseName("ix_acc_currencies_base");

            builder.HasMany(cj => cj.Denominations)
                .WithOne(c => c.Currency)
                .HasForeignKey(cj => cj.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cj => cj.ExchangeRates)
                .WithOne(c => c.Currency)
                .HasForeignKey(cj => cj.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cj => cj.LedgerAccounts)
                .WithOne(c => c.Currency)
                .HasForeignKey(cj => cj.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cj => cj.BankAccounts)
                .WithOne(c => c.Currency)
                .HasForeignKey(cj => cj.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cj => cj.RevolvingFunds)
                .WithOne(c => c.Currency)
                .HasForeignKey(cj => cj.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
