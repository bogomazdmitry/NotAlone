using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NotAlone.Services;
using NotAlone.WebApp.Models;

namespace NotAlone.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        private readonly ILoverHandlerService _loverHandleService;

        public HomeController(ILogger<HomeController> logger, ILoverHandlerService loverHandlerService)
        {
            _logger = logger;
            _loverHandleService = loverHandlerService;
        }

        
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index(string firstPersonInfo, string secondPersonInfo)
        {
            try
            {
                _loverHandleService.HandlePeople(firstPersonInfo, secondPersonInfo);
                return Ok("Успешно!");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}