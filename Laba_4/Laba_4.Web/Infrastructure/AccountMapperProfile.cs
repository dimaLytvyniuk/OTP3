using AutoMapper;
using Laba_4.BLL.DTO;
using Laba_4.Web.Models.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.Web.Infrastructure
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<RegisterModel, UserRegistrationDto>();
            CreateMap<LoginModel, UserLoginDto>();
        }
    }
}
