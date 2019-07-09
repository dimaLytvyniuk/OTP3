using Laba_4.DAL.EF;
using Laba_4.DAL.Entities;
using Laba_4.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4.DAL.Repositories
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly StockContext dbContext;
        private readonly DbSet<Order> dbSet;

        public OrderRepository(StockContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Orders;
        }

        public void Add(Order entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Order> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(Order entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Order> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public Order Find(Func<Order, bool> predicate)
        {
            return dbSet
                .Include(q => q.User)
                .Include(q => q.Product)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Order> FindAll(Func<Order, bool> predicate)
        {
            return dbSet
                .Include(q => q.User)
                .Include(q => q.Product)
                .Where(predicate)
                .OrderBy(q => q.Date)
                .ToList();
        }

        public async Task<Order> FindAsync(Expression<Func<Order, bool>> predicate)
        {
            return await dbSet
                .Include(q => q.User)
                .Include(q => q.Product)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Order> GetAll()
        {
            return dbSet
                .Include(q => q.User)
                .Include(q => q.Product)
                .OrderBy(q => q.Date)
                .ToList();
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await dbSet
                .Include(q => q.User)
                .Include(q => q.Product)
                .ToListAsync();
        }

        public void Update(Order entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
