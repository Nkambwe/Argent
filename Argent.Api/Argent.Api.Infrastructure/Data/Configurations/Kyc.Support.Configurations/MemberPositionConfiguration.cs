using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class MemberPositionConfiguration : IEntityTypeConfiguration<MemberPosition> {
        public void Configure(EntityTypeBuilder<MemberPosition> builder) {
            builder.ToTable("kyc_member_positions");
            builder.Property(p => p.Notes).HasMaxLength(500);
            builder.Property(p => p.CreatedBy).HasMaxLength(100);
            builder.Property(p => p.UpdatedBy).HasMaxLength(100);
            builder.Property(p => p.DeletedBy).HasMaxLength(100);
            builder.HasOne(p => p.Member).WithMany(m => m.Positions).HasForeignKey(p => p.MemberId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.Position).WithMany().HasForeignKey(p => p.PositionId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
