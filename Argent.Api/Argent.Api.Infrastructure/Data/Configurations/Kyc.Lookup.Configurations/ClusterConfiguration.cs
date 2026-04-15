using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class ClusterConfiguration : IEntityTypeConfiguration<Cluster> {
        public void Configure(EntityTypeBuilder<Cluster> builder) {
            builder.ToTable("kyc_clusters");
            builder.Property(c => c.Series).HasMaxLength(20);
            builder.Property(c => c.ClusterName).IsRequired().HasMaxLength(150);
            builder.Property(c => c.Area).HasMaxLength(200);
            builder.Property(c => c.CreditOfficer).HasMaxLength(200);
            builder.Property(c => c.MergedToCode).HasMaxLength(50);
            builder.Property(c => c.Notes).HasMaxLength(500);
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);
            builder.HasOne(c => c.Group).WithMany(g => g.Clusters).HasForeignKey(c => c.GroupId).OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(c => c.GroupId).HasDatabaseName("ix_clusters_group_id");
        }
    }

}
