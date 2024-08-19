using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class WeatherAlertConfiguration : IEntityTypeConfiguration<WeatherAlert>
    {
        public void Configure(EntityTypeBuilder<WeatherAlert> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.CityName).IsRequired();
            builder.Property(p => p.Message).IsRequired();
            builder.HasOne(c => c.City)
                .WithMany(wc => wc.WeatherAlerts)
                .HasForeignKey(wc => wc.CityId)
                .IsRequired();
            builder.HasMany(wn => wn.WeatherNotifications)
                .WithOne(wa => wa.WeatherAlert)
                .HasForeignKey(wn => wn.WeatherAlertId)
                .IsRequired();
        }
    }
}
