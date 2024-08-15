using iVertion.Application.Interfaces;
using iVertion.Application.Mappings;
using iVertion.Application.Services;
using iVertion.Domain.Account;
using iVertion.Domain.Interfaces;
using iVertion.Infra.Data.Context;
using iVertion.Infra.Data.Identity;
using iVertion.Infra.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace iVertion.Infra.IoC
{
    public static class DependencyInjectionAPI
    {
        public static IServiceCollection AddInfrastructureAPI(this IServiceCollection services,
                                                              IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    // ======================= MySql ==================================
                    // var host = configuration["DBHOST"] ?? "localhost";
                    // var port = configuration["DBPORT"] ?? "3306";
                    // var user = configuration["DBUSER"] ?? "root";
                    // var database = configuration["DBNAME"] ?? "ivertion";
                    // var password = configuration["DBPASSWORD"] ?? "";
 
                    // string connectionString = $"Server={host};Port={port};Database={database};User ID={user}; Password={password};";
                    // options.UseMySql(connectionString,
                    //                 ServerVersion.AutoDetect(connectionString));
                    // ================================================================

                    // ===================== SQL Server Azure =========================
                    var host = configuration["DBHOST"] ?? "localhost";
                    var port = configuration["DBPORT"] ?? "1433";
                    var user = configuration["DBUSER"] ?? "sa";
                    var database = configuration["DBNAME"] ?? "ivertion";
                    var password = configuration["DBPASSWORD"] ?? "";
                    var persistSecurityInfo = configuration["PERSISTSECURITYINFO"] ?? "false";
                    var multipleActiveResultSets = configuration["MULTIPLEACTIVERESULTSETS"] ?? "false";
                    var encrypt = configuration["ENCRYPT"] ?? "false";
                    var trustServerCertificate = configuration["TRUSTSERVERCERTIFICATE"] ?? "false";
                    var connectionTimeout = configuration["CONECTIONTIMEOUT"] ?? "30";
                    
                    //var driver = "Driver={ODBC Driver 18 for SQL Server}";

                    
                    // Local
                    string connectionString = $"Server={host};Database={database};Integrated Security=True;UID={user};PWD={password};TrustServerCertificate=true";
                    // Local to Azure ODBC
                    //string connectionString = $"Server=tcp:{host},{port};Database={database};Uid={user};Pwd={password};Encrypt=yes;TrustServerCertificate=no;Connection Timeout=30;";
                    // ADO.NET 
                    // string connectionString = $"Server={host},{port};Initial Catalog={database};Persist Security Info={persistSecurityInfo};User ID={user};Password={password};MultipleActiveResultSets={multipleActiveResultSets};Encrypt={encrypt};TrustServerCertificate={trustServerCertificate};Connection Timeout={connectionTimeout};";
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                    // ================================================================

                }
            );

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Repositories
            services.AddScoped<IRepository, Repository>();
            
            // Users Profiles and Roles Repositories
            services.AddScoped<ITemporaryUserRoleRepository, TemporaryUserRoleRepository>();
            services.AddScoped<IAdditionalUserRoleRepository, AdditionalUserRoleRepository>();
            services.AddScoped<IUserProfileRepository, UserProfileRepository>();
            services.AddScoped<IRoleProfileRepository, RoleProfileRepository>();

            // Address Repositories
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();


            // Person Repositories
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<IPersonAddressRepository, PersonAddressRepository>();

            // Database Initializer Services

            services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();
            
            // User Autorization Services
            services.AddScoped<IAuthenticate, AuthenticateService>();
            services.AddScoped<ISeedUserRoleInitial, SeedUserRoleInitial>();
            services.AddScoped<IUserInterface<ApplicationUser>, UserService>();
            services.AddScoped<IRoleInterface<IdentityRole>, RoleService>();

            services.AddScoped<IGoogleAuthService<GoogleUserInfo>, GoogleAuthService>();

            // User Profiles Services
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<IRoleProfileService, RoleProfileService>();
            services.AddScoped<IAdditionalUserRoleService, AdditionalUserRoleService>();
            services.AddScoped<ITemporaryUserRoleService, TemporaryUserRoleService>();

            // Person Services
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IPersonAddressService, PersonAddressService>();

            // Address Services
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICountryService, CountryService>();


            // AutoMapper for DTOs
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}