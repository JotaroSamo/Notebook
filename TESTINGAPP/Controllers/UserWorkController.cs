using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TESTINGAPP.Common.Dto;
using TESTINGAPP.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;
using TESTINGAPP.Models;

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
        //[HttpGet]
        //public async Task<IActionResult> AllRecord(int UserId)
        //{

        //    return View(await _recordService.AllRecord(UserId));
        //}
        [Authorize]
        public async Task<IActionResult> AllRecord()
        {
            int UserId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (UserId == 0)
            {
                return NotFound();
            }
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
                return RedirectToAction("AllRecord");
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
            return RedirectToAction("AllRecord");
        }

    }
}
