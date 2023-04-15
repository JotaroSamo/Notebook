using System.Security.Claims;
using Notebook.BusinessLogic.Interfaces;
using Notebook.Common.Dto;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace Notebook.Controllers
{
    public class AuthController : Controller
    {

        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public AuthController(IUserService userService, ILogger<AuthController> logger, IMapper mapper)
        {
            _userService = userService;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult AuthPage()
        {
            _logger.LogInformation($"{DateTime.Now} Method AuthPage has been called.");
            return View();
        }
        public IActionResult RegPage()
        {
            _logger.LogInformation($"{DateTime.Now} Method AuthPage has been called.");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegPage(UserCreateDto model)
        {
            try
            {
                if (await _userService.CheckNull(model)==false)
                {
                    return View(model);
                }
                _logger.LogInformation($"{DateTime.Now} Method RegPage has been called with model {@model}.", model);

                if (await _userService.GetCheckAsync(_mapper.Map<UserCreateDto, CheckUser>(model)) == null)
                {
                    await _userService.CreateAsync(model);
                    _logger.LogInformation("User with name {Name} and email {Email} has been created. {3}", model.Name, model.Email, DateTime.Now);

                    return RedirectToAction("Index", "Home");
                }

                _logger.LogWarning("User with email {Email} already exists. {2}", model.Email, DateTime.Now);

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{DateTime.Now} Error in RegPage with model {@model}.", model);
                throw;
            }
        }
        [HttpPost]
        public async Task<IActionResult> Auth(UserAuthDto userAuthDto)
        {
            try
            {
                var user = await _userService.GetAsync(userAuthDto);
                if (user == null)
                {
                    _logger.LogWarning("Failed login attempt for user {userAuthDto.Email} at {DateTime.Now}", userAuthDto.Email, DateTime.Now);
                    return RedirectToAction("RegPage");
                }

                var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

                if (user.Role.ToString() == "Admin")
                {
                    _logger.LogInformation("Admin with email {userAuthDto.Email} has logged in at {DateTime.Now}", userAuthDto.Email, DateTime.Now);
                    var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
                    return RedirectToAction("Tools", "Admin");
                }

                _logger.LogInformation("User with email {userAuthDto.Email} has logged in at {DateTime.Now}", userAuthDto.Email, DateTime.Now);
                var userClaimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(userClaimsIdentity));
                return RedirectToAction("UserTools", "UserWork");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while logging in user {userAuthDto.Email} at {DateTime.Now}", userAuthDto.Email, DateTime.Now);
                throw;
            }
        }


    }
}
