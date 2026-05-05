using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class AccountReferenceConfiguration : IEntityTypeConfiguration<AccountReference> {
        public void Configure(EntityTypeBuilder<AccountReference> builder) {
            builder.ToTable("acc_references");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Series).HasMaxLength(20);
            builder.Property(r => r.Name).IsRequired().HasMaxLength(150);
            builder.Property(r => r.Notes).HasMaxLength(500);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => r.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_references_code");
        }
    }

}
