using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTracking.Application.Abstractions.Services;
using StockTracking.Application.Background;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities.User;
using StockTracking.Persistence.Background;
using StockTracking.Persistence.Context;
using StockTracking.Persistence.Repositories;
using StockTracking.Persistence.Services;

namespace StockTracking.Persistence
{
    public static class ServiceRegistiration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {
            services.AddDbContext<StockTrackingAPIDbContext>(options =>
     options.UseSqlServer(Configuration.ConnectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IDepotRepository, DepotRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IStockMovementRepository, StockMovementRepository>();
            services.AddScoped<IStockMovementTypeRepository, StockMovementTypeRepository>();


            services.AddSingleton<ICacheService, RedisCacheService>();


            services.AddIdentity<User, Role>(options =>
            {
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireDigit = true;
                options.User.RequireUniqueEmail = true;

            }
            ).AddEntityFrameworkStores<StockTrackingAPIDbContext>();



            services.AddScoped<IUserBackgroundJob, UserBackgroundJob>();
            services.AddHangfire(configuration => configuration
                    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
                    .UseSimpleAssemblyNameTypeSerializer()
                    .UseRecommendedSerializerSettings()
                    .UseSqlServerStorage(Configuration.ConnectionString));


            services.AddHangfireServer();
        }
    }

    public static class Configuration
    {
        static public string ConnectionString
        {
            get
            {
                ConfigurationManager cfg = new ConfigurationManager();
                cfg.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../../Presentation/StockTracking.API"));
                cfg.AddJsonFile("appsettings.json");//microsoft.extensions.configuration.json adındaki paket üst 2 satır için gerekli. çok gerekli

                return cfg.GetConnectionString("MicrosoftSQL");
            }
        }
    }
}
