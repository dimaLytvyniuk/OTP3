using Laba_4.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4.BLL.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();
        Task<ProductDto> CreateProduct(ProductDto productDto);
        Task<ProductDto> GetProduct(int id);
        Task<ProductDto> UpdateProduct(ProductDto productDto);
        Task DeleteProduct(int id);
    }
}
