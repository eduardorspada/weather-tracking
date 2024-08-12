
using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
    {
        public void Configure(EntityTypeBuilder<Neighborhood> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.HasOne(c => c.City)
                .WithMany(n => n.Neighborhoods)
                .HasForeignKey(c => c.CityId)
                .IsRequired();
            builder.HasMany(a => a.Addresses)
                .WithOne(n => n.Neighborhood)
                .HasForeignKey(n => n.NeighborhoodId)
                .IsRequired();
        }
    }
}