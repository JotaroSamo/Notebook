using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TESTINGAPP.BusinessLogic.Interfaces;
using TESTINGAPP.Common.Dto;
using TESTINGAPP.Models;

namespace TESTINGAPP.Controllers
{
    public class AuthController : Controller
    {

        private readonly IUserService _userService;
         
 
        public AuthController(IUserService userService)
        { 
            
            _userService = userService;
        }
        public IActionResult AuthPage()
        {
            return View();
        }
        public IActionResult RegPage()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegPage(UserCreateDto model)
        {

            var check = new UserAuthDto();

            if (await _userService.GetAsync(check) == null)
            {
                await _userService.CreateAsync(model);

                return RedirectToAction("Index", "Home");
            }

            await Response.WriteAsync("Занято");

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Auth(UserAuthDto userAuthDto)
        {
            var user = await _userService.GetAsync(userAuthDto);

            if (user != null)
            {
                var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

                //var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                //    new ClaimsPrincipal(claimIdentity));

                //_logger.LogInformation($"{DateTime.Now}: user with {userAuthDto.Email} is log in");

                return RedirectToAction("Index", "Home");
            }

            return View(userAuthDto);
        }
    }
}
