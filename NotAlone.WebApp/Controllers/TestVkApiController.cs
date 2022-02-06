using System;
using Microsoft.AspNetCore.Mvc;
using NotAlone.Services;
using NotAlone.WebApp.Models;

namespace NotAlone.WebApp.Controllers
{
    [Route("test")]
    public class TestVkApiController : Controller
    {
        private readonly IVkService _vkService;

        public TestVkApiController(IVkService vkService)
        {
            _vkService = vkService;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Index(string message, string recipient)
        {
            TestVkApiViewModel testVkApiViewModel = new TestVkApiViewModel();
            try
            {
                _vkService.SendMessage(message, recipient);
                testVkApiViewModel.SuccessMessage = "Успешно";
            }
            catch (Exception exception)
            {
                testVkApiViewModel.SuccessMessage = "";
                testVkApiViewModel.ErrorMessage = exception.Message;
            }
            return View(testVkApiViewModel);
        }
    }
}