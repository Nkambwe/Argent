using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Configurations {
    public class GroupConfiguration : IEntityTypeConfiguration<Group> {
        public void Configure(EntityTypeBuilder<Group> builder) {
            builder.ToTable("customers_groups");
            builder.Property(g => g.RegisteredName).IsRequired().HasMaxLength(200);

            builder.HasOne(g => g.GroupFilter1)
                .WithMany().HasForeignKey(g => g.GroupFilter1Id).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(g => g.GroupFilter2)
                .WithMany().HasForeignKey(g => g.GroupFilter2Id).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(g => g.RegisteredName).HasDatabaseName("ix_groups_name");
        }
    }
}
