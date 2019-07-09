using AutoMapper;
using Laba_4.BLL.DTO;
using Laba_4.BLL.Infrastructure;
using Laba_4.BLL.Interfaces;
using Laba_4.DAL.Entities;
using Laba_4.DAL.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4.BLL.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrderService(
            IUnitOfWork unitOfWork,
            IAccountService accountService,
            IProductService productService)
        {
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _productService = productService;
            _mapper = new MapperConfiguration(x => x.AddProfile(new OrderMapperProfile())).CreateMapper();
        }

        public async Task<OrderDetailsDto> CreateOrder(OrderCreateDto orderDto)
        {
            using (_unitOfWork)
            {
                var orderRepository = _unitOfWork.OrderRepository;
                var userRepository = _unitOfWork.UserRepository;
                var productRepository = _unitOfWork.ProductRepository;

                var order = _mapper.Map<OrderCreateDto, Order>(orderDto);
                var user = await userRepository.FindAsync(x => x.Email == orderDto.UserEmail);
                var product = await productRepository.FindAsync(x => x.Id == orderDto.ProductId);
                if (user == null || product == null)
                    return null;

                order.User = user;
                order.Product = product;
                order.Date = DateTime.Now.ToUniversalTime();

                if (product.Quantity >= order.Quantity)
                {
                    order.IsDone = true;
                    product.Quantity -= order.Quantity;
                }

                orderRepository.Add(order);
                await _unitOfWork.SaveAsync();
                return await GetOrder(order.Id);
            }
        }

        public async Task<IEnumerable<OrderDetailsDto>> GetAllOrders()
        {
            using (_unitOfWork)
            {
                var orderRepository = _unitOfWork.OrderRepository;

                IEnumerable<Order> orders = await orderRepository.GetAllAsync();
                var ordersDto = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDetailsDto>>(orders);

                return ordersDto;
            }
        }

        public async Task<OrderDetailsDto> GetOrder(int id)
        {
            using (_unitOfWork)
            {
                var orderRepository = _unitOfWork.OrderRepository;

                var order = await orderRepository.FindAsync(x => x.Id == id);
                if (order == null)
                    return null;

                var orderDto = _mapper.Map<Order, OrderDetailsDto>(order);
                return orderDto;
            }
        }
    }
}
