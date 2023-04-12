using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TESTINGAPP.Common.Dto;
using TESTINGAPP.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using TESTINGAPP.Models;
using TESTINGAPP.BusinessLogic.Services;

namespace TESTINGAPP.Controllers
{
    public class UserWorkController : Controller
    {
        private readonly ILogger<UserWorkController> _logger;
        private readonly IRecordService _recordService;

        public UserWorkController(ILogger<UserWorkController> logger, IRecordService recodService)
        {
            _logger = logger;
            _recordService = recodService;

        }
        [Authorize]
        public IActionResult UserTools()
        {
            HttpContext.Session.SetInt32("UserId", int.Parse(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value));
            return View();
        }
        [Authorize]
        public IActionResult AddRecord()
        {
            return View();
        }
        public async Task<IActionResult> GetListUserdRecordAsync()
        {
            int UserId =  HttpContext.Session.GetInt32("UserId") ?? 0;
            if (UserId == 0)
            {
                return NotFound();
            }
            return View("AllRecord", await _recordService.AllRecord(UserId));
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllRecord(int UserId)
        {
            return View(await _recordService.AllRecord(UserId));
        }
        [HttpPost]
        public async Task<IActionResult> Create(RecordCreateDto model)
        {
            try
            {
                int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
                if (UserId == 0)
                {
                    return NotFound();
                }
                await _recordService.RecordCreate(model, UserId);
                return await GetListUserdRecordAsync();
            }
            catch (Exception)
            {

                return NotFound();
            }


        }
        [HttpGet]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            await _recordService.DeleteRecord(id);
            return await GetListUserdRecordAsync();
        }
        [HttpPost]
        public async Task<IActionResult> SearchRecord(string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                var records = await _recordService.SearchAsync(searchString);
                if (records == null)
                {
                    _logger.LogInformation($"No users found for search query {searchString}");
                    return RedirectToAction("GetAllUser");
                }
                _logger.LogInformation($"Retrieved {records.Count()} users for search query {searchString}");
                return View("AllRecord", records);
            }
            return await GetListUserdRecordAsync();
        }
    }
}
