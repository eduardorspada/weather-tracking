

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
            builder.HasMany(a => a.Addresses)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId)
                .IsRequired();
            builder.HasData(
                new City(1, "São Paulo", 11, true),
                new City(2, "Rio de Janeiro", 21, true),
                new City(3, "Brasília", 61, true),
                new City(4, "Salvador", 71, true),
                new City(5, "Fortaleza", 85, true),
                new City(6, "Belo Horizonte", 31, true),
                new City(7, "Curitiba", 41, true),
                new City(8, "Porto Alegre", 51, true),
                new City(9, "Recífe", 81, true),
                new City(10, "Manaus", 92, true)
                );
        }
    }
}