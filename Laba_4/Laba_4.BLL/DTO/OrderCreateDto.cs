using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.BLL.DTO
{
    public class OrderCreateDto
    {
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public string UserEmail { get; set; }
    }
}
