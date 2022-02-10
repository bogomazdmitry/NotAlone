using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotAlone.Services;

namespace NotAlone.WebApp.Controllers
{
    [Route("test")]
    public class TestVkController : Controller
    {
        private readonly IVkService _vkService;

        public TestVkController(IVkService vkService)
        {
            _vkService = vkService;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Index(string message, string recipient)
        {
            try
            {
                await _vkService.SendMessage(message, recipient);
                return Ok("Успешно!");
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}