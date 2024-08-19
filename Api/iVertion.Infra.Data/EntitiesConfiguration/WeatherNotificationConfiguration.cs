using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class WeatherNotificationConfiguration : IEntityTypeConfiguration<WeatherNotification>
    {
        public void Configure(EntityTypeBuilder<WeatherNotification> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.IsRead).IsRequired();
            builder.Property(p => p.RetryCount).IsRequired();
            builder.Property(p => p.SentAt).IsRequired();
            builder.Property(p => p.NextRetryAt).IsRequired();
            builder.HasOne(wa => wa.WeatherAlert)
                .WithMany(wn => wn.WeatherNotifications)
                .HasForeignKey(wn => wn.WeatherAlertId)
                .IsRequired();
            builder.HasOne(d => d.Device)
                .WithMany(wn => wn.WeatherNotifications)
                .HasForeignKey(wn => wn.DeviceId)
                .IsRequired();
        }
    }
}
