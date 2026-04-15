using Argent.Api.Domain.Entities.Support.KycSupport;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class RejectReasonConfiguration : IEntityTypeConfiguration<RejectReason> {
        public void Configure(EntityTypeBuilder<RejectReason> builder) {
            builder.ToTable("kyc_reject_reasons");
            builder.Property(r => r.Code).IsRequired().HasMaxLength(20);
            builder.Property(r => r.Description).IsRequired().HasMaxLength(200);
            builder.Property(r => r.Notes).HasMaxLength(500);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);
            builder.HasIndex(r => r.Code).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_reject_reasons_code");
        }
    }
}
