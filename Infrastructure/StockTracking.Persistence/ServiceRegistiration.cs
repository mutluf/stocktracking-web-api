using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTracking.Application.Repositories;
using StockTracking.Persistence.Context;
using StockTracking.Persistence.Repositories;

namespace StockTracking.Persistence
{
    public static class ServiceRegistiration
    {
        public static void AddPersistenceService(this IServiceCollection services)
        {
            services.AddDbContext<StockTrackingAPIDbContext>(options =>
     options.UseSqlServer(Configuration.ConnectionString));
            services.AddScoped<IProductRepository, ProductRepository>();
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
