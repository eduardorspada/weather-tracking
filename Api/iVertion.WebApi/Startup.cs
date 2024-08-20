using iVertion.Domain.Account;
using iVertion.Infra.Data.Context;
using iVertion.Infra.IoC;
using Newtonsoft.Json;
using Hangfire;
using Hangfire.SqlServer;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using iVertion.WebApi.Controllers;

namespace iVertion.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("weather-tracking-OAuth2.json"),
            });

            var host = Configuration["DBHOST"] ?? "localhost";
            var port = Configuration["DBPORT"] ?? "1433";
            var user = Configuration["DBUSER"] ?? "sa";
            var database = Configuration["DBNAME"] ?? "ivertion";
            var password = Configuration["DBPASSWORD"] ?? "";
            var persistSecurityInfo = Configuration["PERSISTSECURITYINFO"] ?? "false";
            var multipleActiveResultSets = Configuration["MULTIPLEACTIVERESULTSETS"] ?? "false";
            var encrypt = Configuration["ENCRYPT"] ?? "false";
            var trustServerCertificate = Configuration["TRUSTSERVERCERTIFICATE"] ?? "false";
            var connectionTimeout = Configuration["CONECTIONTIMEOUT"] ?? "30";
            
            //var driver = "Driver={ODBC Driver 18 for SQL Server}";

            
            // Local
            string connectionString = $"Server={host};Database={database};User Id={user};Password={password};TrustServerCertificate=true";

            // Configurando o Hangfire com SQL Server
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Adicionando o servidor Hangfire
            services.AddHangfireServer();
            
            services.AddInfrastructureAPI(Configuration, connectionString);
            services.AddInfrastructureJWT(Configuration);
            services.AddInfrastructureSwagger();
            services.AddControllers()
                .AddNewtonsoftJson(options =>
            {
                // Aqui você pode configurar o Newtonsoft.Json, como ignorar loops de referência
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });


            services.AddCors(options =>
            {
                options.AddPolicy("AllowSpecificOrigin", builder =>
                {
                    builder.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
                }
                );
            }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static async void Configure(IApplicationBuilder app, IWebHostEnvironment env, ISeedUserRoleInitial seedUserRoleInitial, IDatabaseInitializer databaseInitializer)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/iVertion/swagger.json", "iVertion Palladium 1.0"));
                app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true) // Allow any origin
                    .AllowCredentials());
            }

            // Configure(
            //     app.ApplicationServices.GetRequiredService<ApplicationDbContext>()
            // );
            
            app.UseHttpsRedirection();
            app.UseStatusCodePages();
            app.UseRouting();
            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<WeatherController>(
                                        "set-weathers-notifications",
                                        x => x.RunSetNextNotificationsAsync(),
                                        Cron.Minutely());  
            RecurringJob.AddOrUpdate<WeatherController>(
                                        "send-weathers-notifications",
                                        x => x.RunSendNotificationAsync(),
                                        Cron.Minutely());  

            seedUserRoleInitial.SeedRoles();
            seedUserRoleInitial.SeedUsers();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();
            });
        }
    }
}