using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace TESTINGAPP.Controllers
{
    public class UserWorkController : Controller
    {
        public IActionResult AddRecord()
        {
            return View();
        }
        public IActionResult AllRecord()
        {
            return View();
        }
    }
}
