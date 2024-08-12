using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.HasOne(c => c.Country)
                .WithMany(s => s.States)
                .HasForeignKey(c => c.CountryId)
                .IsRequired();
            builder.HasMany(c => c.Cities)
                .WithOne(s => s.State)
                .HasForeignKey(s => s.StateId)
                .IsRequired();
        }
    }
}