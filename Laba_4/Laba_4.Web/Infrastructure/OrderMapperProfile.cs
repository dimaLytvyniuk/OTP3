using AutoMapper;
using Laba_4.BLL.DTO;
using Laba_4.Web.Models.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laba_4.Web.Infrastructure
{
    public class OrderMapperProfile : Profile
    {
        public OrderMapperProfile()
        {
            CreateMap<OrderCreateModel, OrderCreateDto>();
            CreateMap<OrderDetailsDto, OrderDetailsModel>();
        }
    }
}
