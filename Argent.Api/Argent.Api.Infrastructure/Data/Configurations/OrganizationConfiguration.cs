using Argent.Api.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations {
    public class OrganizationConfiguration : IEntityTypeConfiguration<Organization> {
        public void Configure(EntityTypeBuilder<Organization> builder) {
            builder.ToTable("organizations");
            builder.HasKey(o => o.Id);
            builder.Property(o => o.RegisteredName).IsRequired().HasMaxLength(200);
            builder.Property(o => o.ShortName).IsRequired().HasMaxLength(50);
            builder.Property(o => o.RegistrationNumber).IsRequired().HasMaxLength(100);
            builder.HasIndex(o => o.RegistrationNumber).IsUnique();
            builder.Property(o => o.BusinessLine).IsRequired().HasMaxLength(100);
            builder.Property(o => o.ContactEmail).IsRequired().HasMaxLength(150);
            builder.Property(o => o.IsActive).HasDefaultValue(true);

            //..audit columns from BaseEntity
            builder.Property(o => o.CreatedOn).IsRequired();
            builder.Property(o => o.CreatedBy).HasMaxLength(100);
            builder.Property(o => o.UpdatedBy).HasMaxLength(100);
            builder.Property(o => o.DeletedBy).HasMaxLength(100);

            // Relationship
            builder.HasMany(o => o.Branches)
                .WithOne(b => b.Organization)
                .HasForeignKey(b => b.OrganizationId)
                //..never cascade-delete branches
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
