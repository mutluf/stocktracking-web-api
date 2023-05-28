using Hangfire;
using IdentityServer4.Models;
using Microsoft.Extensions.DependencyInjection;
using StockTracking.Application.Abstractions.Token;
using StockTracking.Application.Background;
using StockTracking.Domain.Entities;
using StockTracking.Domain.Entities.User;
using StockTracking.Infrastructure.SqlTableDependency;


namespace StockTracking.Infrastructure
{
    public static class ServiceRegistiration
    {
        public static void AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenHandler,Infrastructure.Services.Token.TokenHandler>();
            services.AddSingleton<DatabaseSubscription<Product>>();

            services.AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddAspNetIdentity<User>()
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddInMemoryIdentityResources(Config.GetIdentityResources());
        }
    }

    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
        {
            new ApiResource("your_api_name", "Your API")
        };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
        {
            new Client
            {
                ClientId = "api-swagger",
                RequireClientSecret = false,
                AllowedGrantTypes = GrantTypes.Code,
                RequirePkce = true,
                RedirectUris = { "https://localhost:7194/swagger/index.html" },
                AllowedCorsOrigins = { "https://localhost:7194" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "api" }
            }
        };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };
        }
    }
}
