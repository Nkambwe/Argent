using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class CashierBranchAccessConfiguration : IEntityTypeConfiguration<CashierBranchAccess> {
        public void Configure(EntityTypeBuilder<CashierBranchAccess> builder) {
            builder.ToTable("acc_cashier_branch_access");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(cb => new { cb.CashierId, cb.BranchId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_cashier_branch_access");

            builder.HasOne(cb => cb.Cashier)
                .WithMany(c => c.BranchAccess)
                .HasForeignKey(cb => cb.CashierId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cb => cb.Branch)
                .WithMany()
                .HasForeignKey(cb => cb.BranchId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
