using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class FolioTypeConfiguration : IEntityTypeConfiguration<FolioType> {
        public void Configure(EntityTypeBuilder<FolioType> builder) {
            builder.ToTable("acc_folio_types");
            builder.Property(f => f.Code).IsRequired().HasMaxLength(20);
            builder.Property(f => f.TypeName).IsRequired().HasMaxLength(100);
            builder.Property(f => f.CreatedBy).HasMaxLength(100);
            builder.Property(f => f.UpdatedBy).HasMaxLength(100);
            builder.Property(f => f.DeletedBy).HasMaxLength(100);
            builder.HasIndex(f => f.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_folio_types_code");
        }
    }
}
