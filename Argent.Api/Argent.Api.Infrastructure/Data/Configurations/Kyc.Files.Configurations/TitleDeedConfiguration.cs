using Argent.Api.Domain.Entities.Kyc.KycFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Kyc.Files.Configurations {
    public class TitleDeedConfiguration : IEntityTypeConfiguration<TitleDeed> {
        public void Configure(EntityTypeBuilder<TitleDeed> builder) {
            builder.ToTable("kyc_title_deeds");
            builder.Property(t => t.PlotNumber).HasMaxLength(50);
            builder.Property(t => t.Block).HasMaxLength(50);
            builder.Property(t => t.Location).HasMaxLength(200);
        }
    }

}
