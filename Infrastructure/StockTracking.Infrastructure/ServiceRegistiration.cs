using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StockTracking.Application.Abstractions.Token;


namespace StockTracking.Infrastructure
{
    public static class ServiceRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler,Infrastructure.Services.Token.TokenHandler>();
        }
    }
}
