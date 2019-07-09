using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.DAL.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public bool IsDone { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}
