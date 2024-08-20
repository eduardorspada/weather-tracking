using Hangfire;
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
                                                              IConfiguration configuration,
                                                              string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));

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

            // Device Repositories
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            
            // Weather Repositories
            services.AddScoped<IWeatherConditionRepository, WeatherConditionRepository>();
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            services.AddScoped<IWeatherAlertRepository, WeatherAlertRepository>();
            services.AddScoped<IWeatherNotificationRepository, WeatherNotificationRepository>();

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

            // Device Services
            services.AddScoped<IDeviceService, DeviceService>();

            // Address Services
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICountryService, CountryService>();

            // Weather Services
            services.AddScoped<IWeatherConditionService, WeatherConditionService>();
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
            services.AddScoped<IWeatherAlertService, WeatherAlertService>();
            services.AddScoped<IWeatherNotificationService, WeatherNotificationService>();

            // HangFire

            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSqlServerStorage(connectionString));

            services.AddHangfireServer();


            // AutoMapper for DTOs
            services.AddAutoMapper(typeof(DomainToDTOMappingProfile));

            return services;
        }
    }
}