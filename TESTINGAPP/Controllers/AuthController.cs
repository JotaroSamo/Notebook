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
         
        RecordContext _recordContext;
        public AuthController(RecordContext recordContext)
        { 
            _recordContext = recordContext;
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
        public async Task<IActionResult> RegPage(User model)
        {


            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password,
                    Age = model.Age,
                    Role = model.Role
                };

                _recordContext.Users.Add(user);
                await _recordContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }


    }
}
