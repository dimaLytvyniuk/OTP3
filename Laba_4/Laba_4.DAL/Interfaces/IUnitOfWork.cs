using Laba_4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Product> ProductRepository { get; }
        IRepository<Order> OrderRepository { get; }
        IRepository<User> UserRepository { get; }

        void Save();
        Task SaveAsync();
    }
}
