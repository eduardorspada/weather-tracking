using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class PersonAddressConfiguration : IEntityTypeConfiguration<PersonAddress>
    {
        public void Configure(EntityTypeBuilder<PersonAddress> builder)
        {
            builder.HasKey(t => t.Id);
            builder.HasOne(a => a.Address)
                .WithMany(pa => pa.PersonAddresses)
                .HasForeignKey(c => c.AddressId);
            builder.HasOne(p => p.Person)
                .WithMany(pa => pa.PersonAddresses)
                .HasForeignKey(p => p.PersonId);
        }
    }
}
