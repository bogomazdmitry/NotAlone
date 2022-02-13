using System;
using System.Diagnostics;
using System.Threading.Tasks;
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
        public async Task<IActionResult> Index(MessagincRequest messagincRequest)
        {
            try
            {
                bool blindDateChecker = messagincRequest.IsBlindDate == "on";
                await _loverHandleService.HandlePeople(messagincRequest.FirstPersonInfo, messagincRequest.SecondPersonInfo, blindDateChecker, messagincRequest.LinkBlindDate);
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