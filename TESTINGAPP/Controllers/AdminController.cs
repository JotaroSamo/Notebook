using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TESTINGAPP.BusinessLogic.Interfaces;
using TESTINGAPP.Common.Dto;
using TESTINGAPP.Models;

namespace TESTINGAPP.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        private readonly RecordContext _recordContext;

        public AdminController(ILogger<AdminController> logger, IAdminService adminService, RecordContext recordContext)
        {
            _logger = logger;
            _adminService = adminService;
            _recordContext = recordContext;
        }

        [HttpPost]
        public async Task<IActionResult> EditUser(int id)
        {
            var user = await _adminService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(int id, [Bind("Id,Name,Email,Password,Age,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            try
            {
                await _adminService.UpdateUser(user);
                _logger.LogInformation($"{DateTime.Now} User with ID {user.Id} has been updated.");
            }
            catch (DbUpdateConcurrencyException)
            {

                    return NotFound();
           
            }
            return RedirectToAction("GetAllUser");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.Delete(id);
            _logger.LogInformation($"{DateTime.Now} User with ID {id} has been deleted.");
            return RedirectToAction("GetAllUser");
        }

        public async Task<IActionResult> GetAllUser()
        {
            var users = await _adminService.GetAll();
            _logger.LogInformation($"{DateTime.Now} Retrieved {users.Count()} users.");
            return View(users);
        }

        [HttpPost]
        public async Task<IActionResult> SearchUser(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var user = await _adminService.SearchAsync(searchString);
                if (user == null)
                {
                    return RedirectToAction("GetAllUser");
                }
                return View(user);
            }
            return RedirectToAction("GetAllUser");
        }
    }
}
