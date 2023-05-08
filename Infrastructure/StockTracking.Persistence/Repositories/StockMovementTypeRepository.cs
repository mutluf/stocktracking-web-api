using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;
using StockTracking.Persistence.Context;

namespace StockTracking.Persistence.Repositories
{
    public class StockMovementTypeRepository : GenericRepository<StockMovementType>, IStockMovementTypeRepository
    {
        public StockMovementTypeRepository(StockTrackingAPIDbContext context) : base(context)
        {
        }
    }
}
