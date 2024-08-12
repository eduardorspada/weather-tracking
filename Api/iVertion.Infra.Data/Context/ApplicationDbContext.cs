using iVertion.Domain.Entities;
using iVertion.Infra.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace iVertion.Infra.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        // Users Profiles
        public DbSet<UserProfile>? UserProfiles { get; set; }
        public DbSet<RoleProfile>? RoleProfiles { get; set; }
        public DbSet<AdditionalUserRole>? AdditionalUserRoles { get; set; }
        public DbSet<TemporaryUserRole>? TemporaryUserRoles { get; set; }


        // Person
        public DbSet<Person>? Persons { get; set; }
        public DbSet<PersonAddress>? PersonAddresses { get; set; }


        // Address
        public DbSet<Address>? Addresses { get; set; }
        public DbSet<Neighborhood>? Neighborhoods { get; set; }
        public DbSet<City>? Cities { get; set; }
        public DbSet<State>? States { get; set; }
        public DbSet<Country>? Countries { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}