using Laba_4.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDetailsDto>> GetAllOrders();
        Task<OrderDetailsDto> CreateOrder(OrderCreateDto orderDto);
        Task<OrderDetailsDto> GetOrder(int id);
    }
}
