using Microsoft.AspNetCore.Mvc;

namespace NotAlone.WebApp.Controllers
{
    public class ResponseModelsController : Controller
    {
        public IActionResult Error(string message)
        {
            return PartialView("_ErrorPartial", message);
        }
        
        public IActionResult Success(string message)
        {
            return PartialView("_SuccessPartial", message);
        }
    }
}