using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class BankConfiguration : IEntityTypeConfiguration<Bank> {
        public void Configure(EntityTypeBuilder<Bank> builder) {
            builder.ToTable("acc_banks");
            builder.Property(b => b.Code).IsRequired().HasMaxLength(20);
            builder.Property(b => b.Name).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Contact).HasMaxLength(200);
            builder.Property(b => b.Telephone).HasMaxLength(30);
            builder.Property(b => b.Email).HasMaxLength(150);
            builder.Property(b => b.Fax).HasMaxLength(30);
            builder.Property(b => b.CreatedBy).HasMaxLength(100);
            builder.Property(b => b.UpdatedBy).HasMaxLength(100);
            builder.Property(b => b.DeletedBy).HasMaxLength(100);
            builder.HasIndex(b => b.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_banks_code");
            builder.HasOne(b => b.Iban).WithMany(i => i.Banks)
                .HasForeignKey(b => b.IbanId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(b => b.Swift).WithMany(s => s.Banks)
                .HasForeignKey(b => b.SwiftId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
