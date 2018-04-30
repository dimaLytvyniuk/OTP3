using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laba_4.Web.Models.Order
{
    public class OrderCreateModel
    {
        [Range(1, 10000, ErrorMessage = "Should be from 1 to 10000")]
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string UserEmail { get; set; }
    }
}
