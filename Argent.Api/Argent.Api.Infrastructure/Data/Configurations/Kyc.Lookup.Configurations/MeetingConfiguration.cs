using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Lookup.Configurations {
    public class MeetingConfiguration : IEntityTypeConfiguration<Meeting> {
        public void Configure(EntityTypeBuilder<Meeting> builder) {
            builder.ToTable("kyc_meetings");
            builder.Property(m => m.Venue).HasMaxLength(200);
            builder.Property(m => m.Agenda).HasMaxLength(1000);
            builder.Property(m => m.Minutes).HasColumnType("text");
            builder.Property(m => m.Notes).HasMaxLength(500);
            builder.Property(m => m.CreatedBy).HasMaxLength(100);
            builder.Property(m => m.UpdatedBy).HasMaxLength(100);
            builder.Property(m => m.DeletedBy).HasMaxLength(100);
            builder.HasOne(m => m.Group).WithMany(g => g.Meetings).HasForeignKey(m => m.GroupId).OnDelete(DeleteBehavior.Cascade);
        }
    }

}
