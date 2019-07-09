using Laba_4.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.DAL.EF
{
    public class StockContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        
        public StockContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
