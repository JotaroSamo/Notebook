using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TESTINGAPP.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult AuthPage()
        {
            return View();
        }
        public IActionResult RegPage()
        {
            return View();
        }
    }
}
