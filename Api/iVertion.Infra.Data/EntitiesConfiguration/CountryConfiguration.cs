using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Name).HasMaxLength(15).IsRequired();
            builder.Property(p => p.Acronym).HasMaxLength(5).IsRequired();
            builder.Property(p => p.Code).IsRequired();
            builder.HasMany(a => a.Addresses)
                .WithOne(c => c.Country)
                .HasForeignKey(c => c.CountryId)
                .IsRequired();

            builder.HasData(
                new Country(1, "Brasil", "BR", 55, true)
                );
        }
    }
}