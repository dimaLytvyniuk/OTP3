using AutoMapper;
using Laba_4.BLL.DTO;
using Laba_4.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Laba_4.BLL.Infrastructure
{
    public class AccountMapperProfile : Profile
    {
        public AccountMapperProfile()
        {
            CreateMap<UserRegistrationDto, User>();
            CreateMap<User, UserProfileDto>();
            CreateMap<UserRegistrationDto, UserLoginDto>();
        }
    }
}
