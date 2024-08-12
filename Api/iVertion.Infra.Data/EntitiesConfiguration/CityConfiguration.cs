

using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.HasOne(s => s.State)
                .WithMany(a => a.Cities)
                .HasForeignKey(s => s.StateId)
                .IsRequired();
            builder.HasMany(n => n.Neighborhoods)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId)
                .IsRequired();
        }
    }
}