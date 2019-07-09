using Laba_4.DAL.Entities;
using Laba_4.DAL.Interfaces;
using Laba_4.DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4.DAL.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly StockContext dbContext;

        private ProductRepository productRepository;
        private OrderRepository queueOrderRepository;
        private UserRepository userRepository;

        public EFUnitOfWork(DbContextOptions options)
        {
            dbContext = new StockContext(options);
        }

        public IRepository<Product> ProductRepository
        {
            get
            {
                if (productRepository == null)
                {
                    productRepository = new ProductRepository(dbContext);
                }
                return productRepository;
            }
        }

        public IRepository<Order> OrderRepository
        {
            get
            {
                if (queueOrderRepository == null)
                {
                    queueOrderRepository = new OrderRepository(dbContext);
                }
                return queueOrderRepository;
            }
        }

        public IRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new UserRepository(dbContext);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    dbContext.Dispose();
                }
            }
            this.disposed = true;
        }
    }
}
