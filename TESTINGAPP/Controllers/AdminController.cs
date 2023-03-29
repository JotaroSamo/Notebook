using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTINGAPP.BusinessLogic.Interfaces;
using TESTINGAPP.Models;

namespace TESTINGAPP.Controllers
{
    public class AdminController : Controller
    {
      private readonly IAdminService _adminService;
        private readonly RecordContext _recordContext;
        public AdminController(IAdminService adminService, RecordContext recordContext)
        {
            _adminService = adminService;
            _recordContext = recordContext;
        }

        public async Task<IActionResult> GetAllUser()
        {
            return View(await _adminService.GetAll());
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
           
            await _adminService.Delete(id);
            return RedirectToAction("GetAllUser");
        }
    }
}
