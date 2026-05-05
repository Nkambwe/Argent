using Argent.Api.Domain.Entities.Accounting.Vouchers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class CashierVoucherTypeConfiguration : IEntityTypeConfiguration<CashierVoucherType> {
        public void Configure(EntityTypeBuilder<CashierVoucherType> builder) {
            builder.ToTable("acc_cashier_voucher_types");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(cv => new { cv.CashierId, cv.VoucherTypeId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_cashier_voucher_types");

            builder.HasOne(cv => cv.Cashier)
                .WithMany(c => c.VoucherTypes)
                .HasForeignKey(cv => cv.CashierId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cv => cv.VoucherType)
                .WithMany(vt => vt.Cashiers)
                .HasForeignKey(cv => cv.VoucherTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }

}
