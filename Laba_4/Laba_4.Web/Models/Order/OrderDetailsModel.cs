using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba_4.Web.Models.Order
{
    public class OrderDetailsModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public DateTime Date { get; set; }
        public string UserEmail { get; set; }
        public string Status { get; set; }
        public decimal Price { get; set; }
    }
}
