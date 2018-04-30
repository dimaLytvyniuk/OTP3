using AutoMapper;
using Laba_4.BLL.DTO;
using Laba_4.Web.Models.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba_4.Web.Infrastructure
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<ProductModel, ProductDto>();
            CreateMap<ProductDto, ProductModel>();
        }
    }
}