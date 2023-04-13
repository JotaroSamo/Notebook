﻿using Microsoft.AspNetCore.Http;
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
            _logger.LogInformation($"[{DateTime.UtcNow.ToString()}] UserTools action called");
            return View();
        }

        [Authorize]
        public IActionResult AddRecord()
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToString()}] AddRecord action called");
            return View();
        }

        public async Task<IActionResult> GetListUserdRecordAsync()
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (UserId == 0)
            {
                return NotFound();
            }

            _logger.LogInformation($"[{DateTime.UtcNow.ToString()}] GetListUserdRecordAsync action called for user with Id={UserId}");
            return View("AllRecord", await _recordService.AllRecord(UserId));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AllRecord(int UserId)
        {
            _logger.LogInformation($"[{DateTime.UtcNow.ToString()}] AllRecord action called for user with Id={UserId}");
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
        public async Task<IActionResult> SaveEditRecordAsync(RecordCreateDto model, int id, int UserId)
        {
            _logger.LogInformation($"[{DateTime.Now}] SaveEditRecordAsync action called for record with Id={id} and user with Id={UserId}");
            await _recordService.EditRecord(model, id, UserId);
            return await GetListUserdRecordAsync();
        }

        [HttpGet]
        public async Task<IActionResult> EditRecord(int id, int UserId)
        {
            ViewData["Id"] = id;
            ViewData["UserId"] = UserId;
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
