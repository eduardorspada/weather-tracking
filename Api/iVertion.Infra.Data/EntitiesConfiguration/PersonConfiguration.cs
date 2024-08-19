
using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.FirstName).HasMaxLength(25).IsRequired();
            builder.Property(p => p.LastName).HasMaxLength(255).IsRequired();
            builder.Property(p => p.Birthday).IsRequired();
            builder.Property(p => p.ProfilePicture).IsRequired();
            builder.HasMany(pa => pa.PersonAddresses)
                .WithOne(p => p.Person)
                .HasForeignKey(pa => pa.PersonId)
                .IsRequired();
            builder.HasMany(d => d.Devices)
                .WithOne(p => p.Person)
                .HasForeignKey(p => p.PersonId)
                .IsRequired();
        }
    }
}