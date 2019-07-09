using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba_4.DAL.EF;
using Laba_4.DAL.Entities;
using Laba_4.Web.Models.Order;
using AutoMapper;
using Laba_4.BLL.Interfaces;
using Laba_4.Web.Infrastructure;
using Laba_4.BLL.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Laba_4.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public OrdersController(IOrderService orderService, IProductService productService)
        {
            _orderService = orderService;
            _productService = productService;
            _mapper = new MapperConfiguration(x => x.AddProfile(new OrderMapperProfile())).CreateMapper();
        }

        // GET: QueueOrders
        public async Task<IActionResult> Index()
        {
            IEnumerable<OrderDetailsDto> ordersDto = await _orderService.GetAllOrders();
            var orders = _mapper.Map<IEnumerable<OrderDetailsDto>, IEnumerable<OrderDetailsModel>>(ordersDto);
            return View(orders);
        }

        // GET: QueueOrders/Create
        public async Task<IActionResult> Create(int id)
        {
            var productDto = await _productService.GetProduct(id);
            if (productDto == null)
                return NotFound();

            var order = new OrderCreateModel { ProductId = id, ProductName = productDto.Name };
            return View(order);
        }

        // POST: QueueOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int id, [Bind("Quantity")] OrderCreateModel order)
        {
            if (ModelState.IsValid)
            {
                order.ProductId = id;
                order.UserEmail = User.Identity.Name;
                var orderDto = _mapper.Map<OrderCreateModel, OrderCreateDto>(order);
                await _orderService.CreateOrder(orderDto);
                return RedirectToAction(nameof(Index));
            }
            
            return View(order);
        }
    }
}
