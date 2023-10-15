using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using StockTracking.Application.Repositories;
using StockTracking.Domain.Entities;
using StockTracking.Persistence.Context;
using System.Linq.Expressions;

namespace StockTracking.Persistence.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly StockTrackingAPIDbContext _context;
        public GenericRepository(StockTrackingAPIDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();

        public async Task<bool> AddAysnc(T Model)
        {
            EntityEntry entityEntry = await Table.AddAsync(Model);
            return entityEntry.State == EntityState.Added;
        }

        public void Delete(T Model)
        {
            Table.Remove(Model);
        }

        public async Task<int> SaveAysnc()
        {
            return await _context.SaveChangesAsync();
        }

        public bool Update(T Model)
        {
            EntityEntry entityEntry = _context.Update(Model);
            return entityEntry.State == EntityState.Modified;
        }

        public IQueryable<T> GetAll()
        {
            var query = Table.AsQueryable().AsNoTracking();
            return query;
        }
        

        public async Task<T?> GetByIdAysnc(string id)
        {
            var query = Table.AsQueryable().AsNoTracking() ;
            return await query.FirstOrDefaultAsync(data => data.Id == int.Parse(id));
        }

        public async Task<T?> GetSingleAysnc(Expression<Func<T, bool>> method)
        {
            var query = await Table.AsQueryable().AsNoTracking().FirstOrDefaultAsync(method);
            return query;
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method)
        {
            var query = Table.Where(method).AsQueryable().AsNoTracking();
            return query;
        }

    }
}
