using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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


    }
}
