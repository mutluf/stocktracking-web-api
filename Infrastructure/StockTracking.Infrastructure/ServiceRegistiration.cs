using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using StockTracking.Application.Abstractions.Token;
using StockTracking.Domain.Entities;
using StockTracking.Infrastructure.SqlTableDependency;
using System;


namespace StockTracking.Infrastructure
{
    public static class ServiceRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler,Infrastructure.Services.Token.TokenHandler>();
            services.AddSingleton<DatabaseSubscription<Product>>();
        }
    }
}
