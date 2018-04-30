using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Laba_4.Web.Models.Product
{
    public class ProductModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1, 10000, ErrorMessage = "Should be from 1 to 10000")]
        public int Quantity { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
