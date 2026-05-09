using Argent.Api.Domain.Entities.Accounting.Cashflow;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Argent.Api.Infrastructure.Data.Configurations.Accounting.Configurations {
    public class CardConfiguration : IEntityTypeConfiguration<Card> {
        public void Configure(EntityTypeBuilder<Card> builder) {
            builder.ToTable("acc_cards");
            builder.Property(c => c.Holder).IsRequired().HasMaxLength(200);
            builder.Property(c => c.CardNumber).IsRequired().HasMaxLength(512);
            builder.Property(c => c.Limit).HasColumnType("decimal(18,2)");
            builder.Property(c => c.CreatedBy).HasMaxLength(100);
            builder.Property(c => c.UpdatedBy).HasMaxLength(100);
            builder.Property(c => c.DeletedBy).HasMaxLength(100);

            builder.HasOne(cj => cj.Vendor)
                .WithMany(c => c.Cards)
                .HasForeignKey(cj => cj.VendorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(cj => cj.CardLedgerEntries)
                .WithOne(c => c.Card)
                .HasForeignKey(cj => cj.CardId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
