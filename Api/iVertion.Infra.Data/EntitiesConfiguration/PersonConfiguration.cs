
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
            builder.Property(p => p.ProfilePicture).HasMaxLength(255).IsRequired();

            builder.HasData(
                new Person(1, "Administrator", "System", new DateTime(2000, 1, 1, 0, 0, 0), "images\\defautuserimage.png")
            );
        }
    }
}