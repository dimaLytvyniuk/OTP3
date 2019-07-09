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
    public class UserRepository : IRepository<User>
    {
        private readonly StockContext dbContext;
        private readonly DbSet<User> dbSet;

        public UserRepository(StockContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = this.dbContext.Users;
        }

        public void Add(User entity)
        {
            dbSet.Add(entity);
        }

        public void AddRange(IEnumerable<User> entities)
        {
            foreach (var entity in entities)
                Add(entity);
        }

        public void Delete(User entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<User> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public User Find(Func<User, bool> predicate)
        {
            return dbSet
               .Include(u => u.Orders)
               .FirstOrDefault(predicate);
        }

        public IEnumerable<User> FindAll(Func<User, bool> predicate)
        {
            return dbSet
                .Include(u => u.Orders)
                .Where(predicate)
                .ToList();
        }

        public async Task<User> FindAsync(Expression<Func<User, bool>> predicate)
        {
            return await dbSet
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(predicate);
        }

        public IEnumerable<User> GetAll()
        {
            return dbSet
                .Include(u => u.Orders)
                .ToList();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbSet
                .Include(u => u.Orders)
                .ToListAsync();
        }

        public void Update(User entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}
