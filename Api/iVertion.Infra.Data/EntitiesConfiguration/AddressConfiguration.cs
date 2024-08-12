using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class AddressConfiguration : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.ZipCode).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Street).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Number).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Complement).HasMaxLength(150);
            builder.HasOne(n => n.Neighborhood)
                .WithMany(a => a.Addresses)
                .HasForeignKey(n => n.NeighborhoodId)
                .IsRequired();
        }
    }
}