using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Notebook.Common.Dto;
using Notebook.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Notebook.Models;
using Notebook.BusinessLogic.Services;

namespace Notebook.Controllers
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

        private int GetCurrentUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        [Authorize]
        public IActionResult UserTools()
        {
            HttpContext.Session.SetInt32("UserId", GetCurrentUserId());
            _logger.LogInformation($"[{DateTime.Now}] UserTools action called");
            return View();
        }

        [Authorize]
        public IActionResult AddRecord()
        {
            _logger.LogInformation($"[{DateTime.Now}] AddRecord action called");
            return View();
        }

        public async Task<IActionResult> GetListUserdRecordAsync()
        {

            int UserId = GetCurrentUserId();
            if (UserId == 0)
            {
                return NotFound();
            }
            _logger.LogInformation($"[{DateTime.Now}] GetListUserdRecordAsync action called for user with Id={UserId}");
            return View("AllRecord", await _recordService.AllRecord(UserId));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllRecord(int UserId)
        {
            _logger.LogInformation($"[{DateTime.Now}] AllRecord action called for user with Id={UserId}");
            return View(await _recordService.AllRecord(UserId));
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecordDto model, IFormFile file)
        {
            try
            {
                int UserId = GetCurrentUserId();
                if (UserId == 0)
                {
                    return NotFound();
                }
                if (file!=null)
                {
                    model.Photo = await _recordService.ConvertToByteArray(file);
                }
                _logger.LogInformation($"[{DateTime.Now}] Create action called for user with Id={UserId}");
                await _recordService.RecordCreate(model, UserId);
                return await GetListUserdRecordAsync();
            }
            catch (Exception)
            {
                _logger.LogError($"[{DateTime.Now}] Create action failed");
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> SaveEditRecord(RecordDto model, int id, IFormFile file)
        {
            int UserId = GetCurrentUserId();
            if (file != null)
			{
				model.Photo = await _recordService.ConvertToByteArray(file);
			}
			_logger.LogInformation($"[{DateTime.Now}] SaveEditRecordAsync action called for record with Id={id} and user with Id={UserId}");
			await _recordService.EditRecord(model, id, UserId);
            return await GetListUserdRecordAsync();
        }

        [HttpGet]
        public async Task<IActionResult> EditRecord(int id, int UserId)
        {
           
            ViewData["Id"] = id;
            _logger.LogInformation($"[{DateTime.Now}] EditRecord action called for record with Id={id} and user with Id={UserId}");
            return View(await _recordService.GetRecordDtoById(id));
        }

        [HttpGet]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            _logger.LogInformation($"[{DateTime.Now}] DeleteRecord action called for record with Id={id}");
            await _recordService.DeleteRecord(id);
            return await GetListUserdRecordAsync();
        }
        [HttpPost]
        public async Task<IActionResult> SearchRecord(string searchString)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    var records = await _recordService.SearchAsync(searchString);
                    if (records == null)
                    {
                        _logger.LogInformation($"[{DateTime.Now}] No records found for search query {searchString}");
                        return RedirectToAction("GetListUserdRecordAsync");
                    }
                    _logger.LogInformation($"[{DateTime.Now}] Retrieved {records.Count()} records for search query {searchString}");
                    return View("AllRecord", records);
                }
                return await GetListUserdRecordAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"[{DateTime.Now}] Error occurred while searching records: {ex.Message}");
                return NotFound();
            }
        }

    }
}
