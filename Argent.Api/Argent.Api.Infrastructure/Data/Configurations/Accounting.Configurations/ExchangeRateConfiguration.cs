using Argent.Api.Domain.Entities.Accounting.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate> {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder) {
            builder.ToTable("acc_exchange_rates");
            builder.Property(r => r.Against).IsRequired().HasMaxLength(10);
            builder.Property(r => r.Buy).HasColumnType("decimal(18,6)");
            builder.Property(r => r.Sale).HasColumnType("decimal(18,6)");
            builder.Property(r => r.Average).HasColumnType("decimal(18,6)");
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            // Only one running rate per currency-pair at a time
            builder.HasIndex(r => new { r.CurrencyId, r.Against, r.IsRunning })
                .HasDatabaseName("ix_acc_exchange_rates_currency_pair_running");

            builder.HasOne(r => r.Currency)
                .WithMany(c => c.ExchangeRates)
                .HasForeignKey(r => r.CurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
