using AutoMapper;
using Laba_4.BLL.DTO;
using Laba_4.BLL.Infrastructure;
using Laba_4.BLL.Interfaces;
using Laba_4.DAL.Entities;
using Laba_4.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _mapper = new MapperConfiguration(x => x.AddProfile(new ProductMapperProfile())).CreateMapper();
        }

        public async Task<ProductDto> CreateProduct(ProductDto productDto)
        {
            using (_unitOfWork)
            {
                var productRepository = _unitOfWork.ProductRepository;

                var product = _mapper.Map<ProductDto, Product>(productDto);
                productRepository.Add(product);
                await _unitOfWork.SaveAsync();
                return await GetProduct(product.Id);
            }
        }

        public async Task DeleteProduct(int id)
        {
            using (_unitOfWork)
            {
                var productRepository = _unitOfWork.ProductRepository;
                var product = await productRepository.FindAsync(x => x.Id == id);

                if (product != null)
                {
                    productRepository.Delete(product);
                    await _unitOfWork.SaveAsync();
                }
            }
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            using (_unitOfWork)
            {
                var productRepository = _unitOfWork.ProductRepository;
                IEnumerable<Product> products = await productRepository.GetAllAsync();
                var productsDto = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(products);
                return productsDto;
            }
        }

        public async Task<ProductDto> GetProduct(int id)
        {
            using (_unitOfWork)
            {
                var productRepository = _unitOfWork.ProductRepository;
                var product = await productRepository.FindAsync(x => x.Id == id);
                if (product == null)
                    return null;
                var productDto = _mapper.Map<Product, ProductDto>(product);
                return productDto;
            }
        }

        public async Task<ProductDto> UpdateProduct(ProductDto productDto)
        {
            using (_unitOfWork)
            {
                var productRepository = _unitOfWork.ProductRepository;
                var orderRepository = _unitOfWork.OrderRepository;

                var product = await productRepository.FindAsync(x => x.Id == productDto.Id);
                if (product == null)
                    return null;

                product = _mapper.Map<ProductDto, Product>(productDto, product);
                IEnumerable<Order> orders = orderRepository.FindAll(x => x.ProductId == product.Id && x.IsDone == false);

                foreach (var order in orders)
                {
                    if (order.Quantity <= product.Quantity)
                    {
                        order.IsDone = true;
                        product.Quantity -= order.Quantity;
                        orderRepository.Update(order);
                    }
                }

                // productRepository.Update(product);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<Product, ProductDto>(product);
            }
        }

        //private async Task UpdatedProduct(int productId)
        //{
        //    using (_unitOfWork)
        //    {
        //        var orderRepository = _unitOfWork.OrderRepository;
        //        var productRepository = _unitOfWork.ProductRepository;

        //        IEnumerable<Order> orders = orderRepository.FindAll(x => x.ProductId == productId);
        //        var product = await GetProduct(productId);

        //        foreach (var order in orders)
        //        {
        //            if (order.Quantity <= product.Quantity)
        //            {
        //                order.IsDone = true;
        //                product.Quantity -= order.Quantity;
        //                await _unitOfWork.SaveAsync();
        //                break;
        //            }
        //        }
        //    }
        //}
    }
}
