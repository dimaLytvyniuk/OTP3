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
    public class ProductRepository : IRepository<Product>
    {
        private readonly StockContext dbContext;
        private readonly DbSet<Product> dbSet;

        public ProductRepository(StockContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Products;
        }

        public void Add(Product entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<Product> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(Product entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<Product> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public Product Find(Func<Product, bool> predicate)
        {
            return dbSet
                .Include(p => p.Orders)
                .FirstOrDefault(predicate);
        }

        public IEnumerable<Product> FindAll(Func<Product, bool> predicate)
        {
            return dbSet
                .Include(p => p.Orders)
                .Where(predicate)
                .ToList();
        }

        public async Task<Product> FindAsync(Expression<Func<Product, bool>> predicate)
        {
            return await dbSet
                .Include(p => p.Orders)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<Product> GetAll()
        {
            return dbSet
                .Include(p => p.Orders)
                .ToList();
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await dbSet
                .Include(p => p.Orders)
                .ToListAsync();
        }

        public void Update(Product entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
