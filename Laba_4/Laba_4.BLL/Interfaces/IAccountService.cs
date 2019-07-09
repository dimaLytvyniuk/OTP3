using Laba_4.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Laba_4.BLL.Interfaces
{
    public interface IAccountService
    {
        Task<UserProfileDto> RegisterUserAsync(UserRegistrationDto userRegistrationDto);
        Task<UserProfileDto> GetUserProfileInfo(UserLoginDto userLoginDto);
    }
}
