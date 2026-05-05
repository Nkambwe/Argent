using Argent.Api.Domain.Entities.Accounting.Currencies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class DenominationConfiguration : IEntityTypeConfiguration<Denomination> {
        public void Configure(EntityTypeBuilder<Denomination> builder) {
            builder.ToTable("acc_denominations");
            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Symbol).HasMaxLength(20);
            builder.Property(d => d.Value).HasColumnType("decimal(18,4)");
            builder.Property(d => d.CreatedBy).HasMaxLength(100);
            builder.Property(d => d.UpdatedBy).HasMaxLength(100);
            builder.Property(d => d.DeletedBy).HasMaxLength(100);
            builder.HasOne(d => d.Currency)
                .WithMany(c => c.Denominations)
                .HasForeignKey(d => d.CurrencyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
