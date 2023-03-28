using System.Security.Claims;
using TESTINGAPP.BusinessLogic.Interfaces;
using TESTINGAPP.Common.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace TESTINGAPP.Controllers
{
    public class AuthController : Controller
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(IUserService userService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public IActionResult AuthPage()
        {
            _logger.LogInformation("Method AuthPage has been called.");
            return View();
        }
        public IActionResult RegPage()
        {
            _logger.LogInformation("Method AuthPage has been called.");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegPage(UserCreateDto model)
        {
            _logger.LogInformation("Method RegPage has been called with model {@model}.", model);
            var check = new CheckUser()
            {
                Email=model.Email,
                Name=model.Name
               
            };

            if (await _userService.GetCheckAsync(check) == null)
            {
                await _userService.CreateAsync(model);
                _logger.LogInformation("User with name {Name} and email {Email} has been created. {3}", model.Name, model.Email,DateTime.Now);

                return RedirectToAction("Index", "Home");
            }

            _logger.LogWarning("User with email {Email} already exists.", model.Email);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Auth(UserAuthDto userAuthDto)
        {
            _logger.LogInformation("Method Auth has been called with model {@userAuthDto}.", userAuthDto);

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
                _logger.LogInformation($"{DateTime.Now}: user with {userAuthDto.Email} is log in");

                return RedirectToAction("GetAllUser");
            }

            _logger.LogWarning("User with email {Email} has failed authentication.", userAuthDto.Email);
            return View(userAuthDto);
        }
       
        public async Task<IActionResult> GetAllUser()
        {
            return View(await _userService.GetAll());
        }
    }
}
