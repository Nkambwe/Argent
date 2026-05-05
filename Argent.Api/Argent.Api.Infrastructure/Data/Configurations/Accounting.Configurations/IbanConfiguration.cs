using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class IbanConfiguration : IEntityTypeConfiguration<Iban> {
        public void Configure(EntityTypeBuilder<Iban> builder) {
            builder.ToTable("acc_ibans");
            builder.Property(i => i.Code).IsRequired().HasMaxLength(50);
            builder.Property(i => i.Narration).HasMaxLength(200);
            builder.Property(i => i.CreatedBy).HasMaxLength(100);
            builder.Property(i => i.UpdatedBy).HasMaxLength(100);
            builder.Property(i => i.DeletedBy).HasMaxLength(100);
            builder.HasIndex(i => i.Code).IsUnique()
                .HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_acc_ibans_code");
        }
    }
}
