using Argent.Api.Domain.Entities.Kyc.KycBusinesses;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Configurations {
    public class BusinessConfiguration : IEntityTypeConfiguration<Business> {
        public void Configure(EntityTypeBuilder<Business> builder) {
            builder.ToTable("customers_businesses");
            builder.Property(b => b.LegalName).IsRequired().HasMaxLength(200);

            builder.HasOne(b => b.BusinessFilter1)
                .WithMany().HasForeignKey(b => b.BusinessFilter1Id).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(b => b.BusinessFilter2)
                .WithMany().HasForeignKey(b => b.BusinessFilter2Id).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(b => b.LegalName).HasDatabaseName("ix_businesses_name");
        }
    }
}
