
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
                //List<Product> datas;
                List<Product> datas;
                T dataBack = new T();
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var myScopedService = scope.ServiceProvider.GetService<IProductRepository>();

                    //datas= myScopedService.GetAll().ToList();
                    //var datas2 = myScopedService.GetAll().Include(data => data.Category);
                    //foreach (var item in datas2)
                    //{
                    //    item.CategoryName = item.Category.CategoryName;
                    //}
                    //datas = datas2.ToList();


                    dataBack =  e.Entity;


                    //datas = myScopedService.GetAll().ToList();

                }
                await _hubContext.Clients.All.SendAsync("receiveMessage", dataBack);


            };
            _tableDependency.OnError += (o, e) =>
            {

            };
            _tableDependency.Start();


        }
        ~DatabaseSubscription()
        {
            _tableDependency.Stop();
        }


    }
}
