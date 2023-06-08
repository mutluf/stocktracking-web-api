
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;
using StockTracking.Infrastructure.Hubs;
using TableDependency.SqlClient;

namespace StockTracking.Infrastructure.SqlTableDependency
{
    public interface IDatabaseSubscription
    {
        void Configure(string tableName);
    }
    public class DatabaseSubscription<T> : IDatabaseSubscription where T : class, new()
    {
        SqlTableDependency<T> _tableDependency;
        IConfiguration _configuration;
        IHubContext<ProductHub> _hubContext;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public DatabaseSubscription(IConfiguration configuration, IHubContext<ProductHub> hubContext, IServiceScopeFactory serviceScopeFactory)
        {
            _configuration = configuration;
            _hubContext = hubContext;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Configure(string tableName)
        {
            _tableDependency = new SqlTableDependency<T>(_configuration.GetConnectionString("MicrosoftSQL"), tableName);
            _tableDependency.OnChanged += async (o, e) =>
            {
                List<Product> datas;
                T dataBack = new T();
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var myScopedService = scope.ServiceProvider.GetService<IProductRepository>();
                    dataBack =  e.Entity;
                }
                await _hubContext.Clients.All.SendAsync("receiveMessage", dataBack);
            };
            _tableDependency.OnError += (o, e) => {};
            _tableDependency.Start();
        }
        ~DatabaseSubscription()
        {
            _tableDependency.Stop();
        }
    }
}
