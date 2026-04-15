using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Support.Configurations {
    public class MemberTransferConfiguration : IEntityTypeConfiguration<MemberTransfer> {
        public void Configure(EntityTypeBuilder<MemberTransfer> builder) {
            builder.ToTable("kyc_member_transfers");
            builder.Property(t => t.FromGroupCode).HasMaxLength(50);
            builder.Property(t => t.FromMemberCode).HasMaxLength(50);
            builder.Property(t => t.ToGroupCode).HasMaxLength(50);
            builder.Property(t => t.ToMemberCode).HasMaxLength(50);
            builder.Property(t => t.Notes).HasMaxLength(500);
            builder.Property(t => t.CreatedBy).HasMaxLength(100);
            builder.Property(t => t.UpdatedBy).HasMaxLength(100);
            builder.Property(t => t.DeletedBy).HasMaxLength(100);
            builder.HasOne(t => t.Member).WithMany(m => m.Transfers).HasForeignKey(t => t.MemberId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(t => t.Reason).WithMany().HasForeignKey(t => t.ReasonId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
