using Argent.Api.Domain.Entities.Vendors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Vendors.Configurations {
    public class BusinessContactConfiguration : IEntityTypeConfiguration<BusinessContact> {
        public void Configure(EntityTypeBuilder<BusinessContact> builder) {
            builder.ToTable("business_contracts");
            builder.Property(r => r.ContactPerson).IsRequired().HasMaxLength(512);
            builder.Property(r => r.BusinessTitle).IsRequired().HasMaxLength(512);
            builder.Property(r => r.EmailAddress).IsRequired().HasMaxLength(512);
            builder.Property(r => r.PhoneNumber).IsRequired().HasMaxLength(512);
            builder.Property(r => r.CreatedBy).HasMaxLength(100);
            builder.Property(r => r.UpdatedBy).HasMaxLength(100);
            builder.Property(r => r.DeletedBy).HasMaxLength(100);

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.BusinessContacts)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
