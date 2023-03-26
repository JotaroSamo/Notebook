using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TESTINGAPP.Models;


namespace TESTINGAPP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
     
    
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {



            using (var _recordContext = new RecordContext())
            {
                _recordContext.Users.Add(new User() { Name = "a", Age="13", Password="1", Email="1", Role=false});
                _recordContext.SaveChanges();
            }
                
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}