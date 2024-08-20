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
            builder.Property(p => p.Name).HasMaxLength(150).IsRequired();
            builder.HasMany(c => c.Addresses)
                .WithOne(s => s.State)
                .HasForeignKey(s => s.StateId)
                .IsRequired();
            builder.HasData(
                new State(1, "São Paulo", "SP", true),
                new State(2, "Rio de Janeiro", "RJ", true),
                new State(3, "Distrito Federal", "DF", true),
                new State(4, "Bahia", "BA", true),
                new State(5, "Ceará", "CE", true),
                new State(6, "Minas Gerais", "MG", true),
                new State(7, "Paraná", "PR", true),
                new State(8, "Rio Gande do Sul", "RS", true),
                new State(9, "Pernanbuco", "PE", true),
                new State(10, "Amazonas", "AM", true)
                );
        }
    }
}