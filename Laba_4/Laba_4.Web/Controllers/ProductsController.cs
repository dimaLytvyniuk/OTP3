using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba_4.DAL.EF;
using Laba_4.DAL.Entities;
using Laba_4.BLL.Interfaces;
using AutoMapper;
using Laba_4.Web.Infrastructure;
using Laba_4.Web.Models.Product;
using Laba_4.BLL.DTO;
using Microsoft.AspNetCore.Authorization;

namespace Laba_4.Web.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
            _mapper = new MapperConfiguration(x => x.AddProfile(new ProductMapperProfile())).CreateMapper();
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            IEnumerable<ProductDto> productsDto = await _productService.GetAllProducts();
            var products = _mapper.Map<IEnumerable<ProductDto>, IEnumerable<ProductModel>>(productsDto);
            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> Create([Bind("Id,Name,Quantity,Price")] ProductModel product)
        {
            if (ModelState.IsValid)
            {
                var productDto = _mapper.Map<ProductModel, ProductDto>(product);
                await _productService.CreateProduct(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "True")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDto = await _productService.GetProduct((int)id);
            if (productDto == null)
            {
                return NotFound();
            }

            var product = _mapper.Map<ProductDto, ProductModel>(productDto);
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Quantity,Price")] ProductModel product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var productDto = _mapper.Map<ProductModel, ProductDto>(product);
                await _productService.UpdateProduct(productDto);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "True")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productDto = await _productService.GetProduct((int)id); 
            if (productDto == null)
            {
                return NotFound();
            }

            var product = _mapper.Map<ProductDto, ProductModel>(productDto);
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "True")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productService.DeleteProduct(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
