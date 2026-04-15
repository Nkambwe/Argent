using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class ClusterMemberConfiguration : IEntityTypeConfiguration<ClusterMember> {
        public void Configure(EntityTypeBuilder<ClusterMember> builder) {
            builder.ToTable("kyc_cluster_members");
            builder.Property(m => m.Notes).HasMaxLength(500);
            builder.Property(m => m.CreatedBy).HasMaxLength(100);
            builder.Property(m => m.UpdatedBy).HasMaxLength(100);
            builder.Property(m => m.DeletedBy).HasMaxLength(100);
            builder.HasOne(m => m.Cluster)
                .WithMany(c => c.Members)
                .HasForeignKey(m => m.ClusterId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(m => m.Member)
                .WithMany()
                .HasForeignKey(m => m.MemberId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(m => new { m.ClusterId, m.MemberId }).IsUnique().HasFilter("\"IsDeleted\" = false").HasDatabaseName("ux_cluster_members_cluster_member");
        }
    }

}
