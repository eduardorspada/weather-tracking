
using iVertion.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace iVertion.Infra.Data.EntitiesConfiguration
{
    public class TemporaryUserRoleConfiguration : IEntityTypeConfiguration<TemporaryUserRole>
    {
        public void Configure(EntityTypeBuilder<TemporaryUserRole> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(p => p.Role).HasMaxLength(25).IsRequired();
            builder.Property(p => p.TargetUserId).IsRequired();
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.ExpirationDate).IsRequired();
        }

    }
}