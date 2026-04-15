using Argent.Api.Domain.Entities.Kyc.KycGroups;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Configurations {
    public class MemberConfiguration : IEntityTypeConfiguration<Member> {
        public void Configure(EntityTypeBuilder<Member> builder) {
            builder.ToTable("customers_members");
            builder.Property(m => m.MemberNumber).HasMaxLength(50);
            builder.Property(m => m.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(m => m.MiddleName).HasMaxLength(100);
            builder.Property(m => m.LastName).IsRequired().HasMaxLength(100);
            builder.Property(m => m.BirthPlace).HasMaxLength(150);
            builder.Property(m => m.SpouseName).HasMaxLength(150);
            builder.Property(m => m.Mother).HasMaxLength(150);
            builder.Property(m => m.Father).HasMaxLength(150);
            builder.Property(m => m.Photo).HasMaxLength(500);
            builder.Property(m => m.Signature).HasMaxLength(500);

            builder.HasOne(m => m.Group)
                .WithMany(g => g.Members)
                .HasForeignKey(m => m.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(m => m.Title)
                .WithMany().HasForeignKey(m => m.TitleId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(m => m.Nationality)
                .WithMany().HasForeignKey(m => m.NationalityId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(m => m.Profession)
                .WithMany().HasForeignKey(m => m.ProfessionId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(m => m.Education)
                .WithMany().HasForeignKey(m => m.EducationId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(m => m.MemberFilter1)
                .WithMany().HasForeignKey(m => m.MemberFilter1Id).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(m => m.MemberFilter2)
                .WithMany().HasForeignKey(m => m.MemberFilter2Id).OnDelete(DeleteBehavior.SetNull);

            builder.HasIndex(m => m.GroupId).HasDatabaseName("ix_members_group_id");
            builder.HasIndex(m => new { m.LastName, m.FirstName }).HasDatabaseName("ix_members_name");
        }
    }
}
