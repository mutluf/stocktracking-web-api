using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;
using StockTracking.Persistence.Context;

namespace StockTracking.Persistence.Repositories
{
    public class DepotRepository : GenericRepository<Depot>, IDepotRepository
    {
        public DepotRepository(StockTrackingAPIDbContext context) : base(context)
        {
        }
    }
}
