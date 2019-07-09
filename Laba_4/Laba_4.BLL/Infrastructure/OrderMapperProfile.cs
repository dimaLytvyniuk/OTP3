using AutoMapper;
using Laba_4.BLL.DTO;
using Laba_4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.BLL.Infrastructure
{
    class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderCreateDto, Order>();
            CreateMap<Order, OrderDetailsDto>()
                .ForMember("Status", opt => opt.MapFrom(o => o.IsDone ? "Completed" : "In progress"))
                .ForMember("UserEmail", opt => opt.MapFrom(o => o.User.Email))
                .ForMember("ProductName", opt => opt.MapFrom(o => o.Product.Name))
                .ForMember("Price", opt => opt.MapFrom(o => o.Product.Price * o.Quantity));
        }
    }
}
