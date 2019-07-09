using AutoMapper;
using Laba_4.BLL.DTO;
using Laba_4.BLL.Interfaces;
using Laba_4.Web.Infrastructure;
using Laba_4.Web.Models.Account;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Laba_4.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            _mapper = new MapperConfiguration(x => x.AddProfile(new AccountMapperProfile())).CreateMapper();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var userLoginDto = _mapper.Map<LoginModel, UserLoginDto>(model);

                UserProfileDto user = await _accountService.GetUserProfileInfo(userLoginDto);
                if (user != null)
                {
                    await Authenticate(model.Email, user.Admin); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                UserRegistrationDto userRegistration = _mapper.Map<RegisterModel, UserRegistrationDto>(model);
                UserProfileDto userProfile = await _accountService.RegisterUserAsync(userRegistration);

                if (userProfile != null)
                {
                    await Authenticate(model.Email, userProfile.Admin); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
            }
            else
                ModelState.AddModelError("", "Некорректные данные");
            return View(model);
        }

        private async Task Authenticate(string userName, bool isAdmin)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, isAdmin.ToString())
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
