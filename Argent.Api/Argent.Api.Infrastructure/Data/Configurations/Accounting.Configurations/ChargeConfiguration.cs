using Argent.Api.Domain.Entities.Accounting.Charges;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class ChargeConfiguration : IEntityTypeConfiguration<Charge> {
        public void Configure(EntityTypeBuilder<Charge> builder) {
            builder.ToTable("acc_charges");
            builder.Property(c => c.Code).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Series).HasMaxLength(20);
            builder.Property(c => c.ChargeName).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Notes).HasMaxLength(500);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.HasIndex(c => c.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_charges_code");
        }
    }
}
