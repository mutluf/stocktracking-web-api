using Microsoft.EntityFrameworkCore;
using StockTracking.Domain.Entities;

namespace StockTracking.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }


}
