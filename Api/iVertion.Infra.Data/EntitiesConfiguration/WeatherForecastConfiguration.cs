using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
    {
        public void Configure(EntityTypeBuilder<WeatherForecast> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.CityName).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.HasOne(c => c.City)
                .WithMany(wc => wc.WeatherForecasts)
                .HasForeignKey(wc => wc.CityId)
                .IsRequired();
        }
    }
}
