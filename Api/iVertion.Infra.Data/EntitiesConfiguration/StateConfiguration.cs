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
            builder.Property(p => p.Code).IsRequired();
            builder.HasMany(c => c.Addresses)
                .WithOne(s => s.State)
                .HasForeignKey(s => s.StateId)
                .IsRequired();
            builder.HasData(
                new State(1, "São Paulo", "SP", 11, true),
                new State(2, "Rio de Janeiro", "RJ", 21, true),
                new State(3, "Distrito Federal", "DF", 61, true),
                new State(4, "Bahia", "BA", 71, true),
                new State(5, "Ceará", "CE", 85, true),
                new State(6, "Minas Gerais", "MG", 31, true),
                new State(7, "Paraná", "PR", 41, true),
                new State(8, "Rio Gande do Sul", "RS", 51, true),
                new State(9, "Pernanbuco", "PE", 81, true),
                new State(10, "Amazonas", "AM", 92, true)
                );
        }
    }
}