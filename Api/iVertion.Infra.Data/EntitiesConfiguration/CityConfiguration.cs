

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
            builder.HasMany(a => a.Addresses)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId)
                .IsRequired();
            builder.HasMany(wc => wc.WeatherConditions)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId)
                .IsRequired();
            builder.HasMany(wf => wf.WeatherForecasts)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId)
                .IsRequired();
            builder.HasMany(wa => wa.WeatherAlerts)
                .WithOne(c => c.City)
                .HasForeignKey(c => c.CityId)
                .IsRequired();
            builder.HasData(
                new City(1, "São Paulo", true),
                new City(2, "Rio de Janeiro", true),
                new City(3, "Brasília", true),
                new City(4, "Salvador", true),
                new City(5, "Fortaleza", true),
                new City(6, "Belo Horizonte", true),
                new City(7, "Curitiba", true),
                new City(8, "Porto Alegre", true),
                new City(9, "Recífe", true),
                new City(10, "Manaus", true)
                );
        }
    }
}