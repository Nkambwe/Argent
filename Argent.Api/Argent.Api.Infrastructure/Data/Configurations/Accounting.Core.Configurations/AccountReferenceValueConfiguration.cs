using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class AccountReferenceValueConfiguration : IEntityTypeConfiguration<AccountReferenceValue> {
        public void Configure(EntityTypeBuilder<AccountReferenceValue> builder) {
            builder.ToTable("acc_reference_values");
            builder.Property(v => v.Code).IsRequired().HasMaxLength(20);
            builder.Property(v => v.Description).IsRequired().HasMaxLength(200);
            builder.Property(v => v.CreatedBy).HasMaxLength(100);
            builder.Property(v => v.UpdatedBy).HasMaxLength(100);
            builder.Property(v => v.DeletedBy).HasMaxLength(100);
            builder.HasIndex(v => new { v.ReferenceId, v.Code }).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_reference_values_ref_code");
            builder.HasOne(v => v.AccountReference)
                .WithMany(r => r.ReferenceValues)
                .HasForeignKey(v => v.ReferenceId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
