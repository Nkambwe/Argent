using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Ledgers.Configurations {
    public class VendorBankAccountConfiguration : IEntityTypeConfiguration<VendorBankAccount> {
        public void Configure(EntityTypeBuilder<VendorBankAccount> builder) {
            builder.ToTable("vendor_bank_accounts");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasIndex(cj => new { cj.VendorId, cj.BankAccountId }).IsUnique()
                .HasFilter("\"IsDeleted\" = false")
                .HasDatabaseName("ux_vendor_bank_accounts");

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.BankAccounts)
                .HasForeignKey(cj => cj.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(cj => cj.BankAccount)
                .WithMany(jt => jt.VendorAcounts)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
