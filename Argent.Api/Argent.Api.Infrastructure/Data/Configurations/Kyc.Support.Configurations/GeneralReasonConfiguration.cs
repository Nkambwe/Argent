using Argent.Api.Domain.Entities.Support.KycSupport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class GeneralReasonConfiguration : IEntityTypeConfiguration<GeneralReason> {
        public void Configure(EntityTypeBuilder<GeneralReason> builder) {
            builder.ToTable("kyc_general_reasons");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Description).IsRequired().HasMaxLength(200);
            builder.Property(r => r.Category).IsRequired().HasMaxLength(50);
            builder.Property(r => r.Notes).HasMaxLength(500);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => new { r.Category, r.Code }).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_general_reasons_category_code");
        }
    }
}
