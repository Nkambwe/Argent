using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class CashierConfiguration : IEntityTypeConfiguration<Cashier> {
        public void Configure(EntityTypeBuilder<Cashier> builder) {
            builder.ToTable("acc_cashiers");
            builder.Property(c => c.Code).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
            builder.Property(c => c.CurrentBranch).HasMaxLength(50);
            builder.Property(c => c.DefaultAccount).HasMaxLength(30);
            builder.Property(c => c.LowerLimit).HasColumnType("decimal(18,2)");
            builder.Property(c => c.UpperLimit).HasColumnType("decimal(18,2)");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(c => c.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_cashiers_code");
        }
    }

}
