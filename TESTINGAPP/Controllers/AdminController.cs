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
      private readonly IAdminService _adminService;
        private readonly RecordContext _recordContext;
        public AdminController(IAdminService adminService, RecordContext recordContext)
        {
            _adminService = adminService;
            _recordContext = recordContext;
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(string searchString)
        //{
        //    var users = from u in _recordContext.Users
        //                select u;
        //    if (!string.IsNullOrEmpty(searchString))
        //    {
        //        users = users.Where(u => u.Name.Contains(searchString) || u.Email.Contains(searchString));
        //    }
        //    var userList = await users.ToListAsync();
        //    return GetAllUser(userList);
        //}

       

        private bool UserExists(int id)
        {
            return _recordContext.Users.Any(e => e.Id == id);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _adminService.GetById(id);
            if (user== null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditDo(int id, [Bind("Id,Name,Email,Password,Age,Role")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }
           
                try
                {
                    _recordContext.Update(user);
                    await _recordContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Home");
       
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.Delete(id);
            return RedirectToAction("GetAllUser");
        }
        public async Task<IActionResult> GetAllUser()
        {
            return View(await _adminService.GetAll());
        }
        //private IActionResult GetAllUser(List<User> userList)
        //{
        //    return View(userList);
        //}
    }
}
