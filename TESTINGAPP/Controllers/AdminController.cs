using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;
using TESTINGAPP.BusinessLogic.Interfaces;
using TESTINGAPP.Common.Dto;
using TESTINGAPP.Models;

namespace TESTINGAPP.Controllers
{
    public class AdminController : Controller
    {
        private readonly ILogger<AdminController> _logger;
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(ILogger<AdminController> logger, IAdminService adminService, IMapper mapper)
        {
            _logger = logger;
            _adminService = adminService;
        _mapper = mapper;

        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditUser(int id)
        {
            _logger.LogInformation($"Trying to edit user with id {id}");
            var user = await _adminService.GetById(id);
            if (user == null)
            {
                _logger.LogInformation($"User with id {id} not found");
                return NotFound();
            }
            _logger.LogInformation($"User with id {id} found");
            return View(user);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateUser(int id, [Bind("Id,Name,Email,Password,Age,Role")] UserDto user)
        {
            if (id != user.Id)
            {
                _logger.LogInformation($"Update failed: user id {id} doesn't match the updated user id {user.Id}");
                return NotFound();
            }

            try
            {
                await _adminService.UpdateUser(user);
                _logger.LogInformation($"{DateTime.Now} User with ID {user.Id} has been updated.");
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("GetAllUser");
                }
                else
                {
                    return RedirectToAction("UserTools", "UserWork");
                }
            
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"Error occurred while updating user with id {user.Id}: {ex.Message}");
                ModelState.AddModelError("", "The user has been updated by another user. Please refresh and try again.");
                return View("EditUser", user);
            }
          
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _adminService.Delete(id);
            _logger.LogInformation($"User with ID {id} has been deleted.");
            return RedirectToAction("GetAllUser");
        }
 
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _adminService.GetAll();
                _logger.LogInformation($"Retrieved {users.Count()} users.");
                return View("ViewAllUser", users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while retrieving users: {ex.Message}");
                throw;
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ViewAllUser(List<UserDto> user)
        {

            return View(user);
        }
        [Authorize(Roles = "Admin")]
       
        public IActionResult Tools()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SearchUser(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var user = await _adminService.SearchAsync(searchString);
                if (user == null)
                {
                    _logger.LogInformation($"No users found for search query {searchString}");
                    return RedirectToAction("GetAllUser");
                }
                _logger.LogInformation($"Retrieved {user.Count()} users for search query {searchString}");
                return View("ViewAllUser", user);
            }
            return RedirectToAction("GetAllUser");
        }
    }
}
