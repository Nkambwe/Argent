using Argent.Api.Domain.Entities.Accounting.Vouchers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class VoucherTypeConfiguration : IEntityTypeConfiguration<VoucherType> {
        public void Configure(EntityTypeBuilder<VoucherType> builder) {
            builder.ToTable("acc_voucher_types");
            builder.Property(v => v.SeriesIdentifier).IsRequired().HasMaxLength(20);
            builder.Property(v => v.VoucherName).IsRequired().HasMaxLength(150);
            builder.Property(v => v.DefaultLedgerNumber).HasMaxLength(30);
            builder.Property(v => v.CreatedBy).HasMaxLength(100);
            builder.Property(v => v.UpdatedBy).HasMaxLength(100);
            builder.Property(v => v.DeletedBy).HasMaxLength(100);

            builder.HasIndex(v => v.VoucherName).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_acc_voucher_types_name");

            builder.HasOne(v => v.GeneralPostingGroup)
                .WithMany()
                .HasForeignKey(v => v.GeneralPostingGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(v => v.BranchPostingGroup)
                .WithMany()
                .HasForeignKey(v => v.BranchPostingGroupId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(v => v.BusinessPostingGroup)
                .WithMany()
                .HasForeignKey(v => v.BusinessPostingGroupId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
