using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace TESTINGAPP.Controllers
{
    public class UserWorkController : Controller
    {
        public IActionResult UserTools()
        {
       
            return View();
        }
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
