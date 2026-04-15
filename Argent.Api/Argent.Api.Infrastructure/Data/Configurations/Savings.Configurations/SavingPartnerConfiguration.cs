using Argent.Api.Domain.Entities.Banking.Savings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Savings.Configurations {
    public class SavingPartnerConfiguration : IEntityTypeConfiguration<SavingPartner> {
        public void Configure(EntityTypeBuilder<SavingPartner> builder) {
            builder.ToTable("kyc_saving_partners");
            builder.Property(s => s.Notes).HasMaxLength(500);
            builder.Property(s => s.CreatedBy).HasMaxLength(100);
            builder.Property(s => s.UpdatedBy).HasMaxLength(100);
            builder.Property(s => s.DeletedBy).HasMaxLength(100);
        }
    }

}
