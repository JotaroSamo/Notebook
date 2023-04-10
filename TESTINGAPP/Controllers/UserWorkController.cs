using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TESTINGAPP.Common.Dto;
using TESTINGAPP.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
       
            return View();
        }
        [Authorize]
        public IActionResult AddRecord()
        {
            return View();
        }
        [Authorize]
        public IActionResult AllRecord()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RecordCreateDto model)
        {
            var userClaimsPrincipal = HttpContext.User;
            var userNameClaim = userClaimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            await _recordService.RecordCreate(model, id: int.Parse(userNameClaim));
            return View("UserTools");

        }

       
    }
}
