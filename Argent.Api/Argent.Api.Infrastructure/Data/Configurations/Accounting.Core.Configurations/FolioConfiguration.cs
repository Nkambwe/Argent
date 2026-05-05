using Argent.Api.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Core.Configurations {
    public class FolioConfiguration : IEntityTypeConfiguration<Folio> {
        public void Configure(EntityTypeBuilder<Folio> builder) {
            builder.ToTable("acc_folios");
            builder.Property(f => f.Code).IsRequired().HasMaxLength(20);
            builder.Property(f => f.Particulars).IsRequired().HasMaxLength(300);
            builder.Property(f => f.Notes).HasMaxLength(500);
            builder.Property(f => f.CreatedBy).HasMaxLength(100);
            builder.Property(f => f.UpdatedBy).HasMaxLength(100);
            builder.Property(f => f.DeletedBy).HasMaxLength(100);
            builder.HasIndex(f => f.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_folios_code");
            builder.HasOne(f => f.FolioType)
                .WithMany(ft => ft.Folios)
                .HasForeignKey(f => f.FolioTypeId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
