using AutoMapper;
using Laba_4.BLL.DTO;
using Laba_4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.BLL.Infrastructure
{
    public class ProductMapperProfile : Profile
    {
        public ProductMapperProfile()
        {
            CreateMap<ProductDto, Product>();
            CreateMap<Product, ProductDto>();
        }

    }
}
