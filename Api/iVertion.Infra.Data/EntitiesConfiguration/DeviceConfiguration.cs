using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(p => p.Token).IsRequired();
            builder.Property(p => p.DeviceName).IsRequired();
            builder.Property(p => p.AcceptNotifications).IsRequired();
            builder.HasOne(p => p.Person)
                .WithMany(d => d.Devices)
                .HasForeignKey(d => d.PersonId)
                .IsRequired();
            builder.HasMany(wn => wn.WeatherNotifications)
                .WithOne(d => d.Device)
                .HasForeignKey(wn => wn.DeviceId)
                .IsRequired();

        }
    }
}
