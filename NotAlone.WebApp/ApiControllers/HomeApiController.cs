using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using NotAlone.Models;
using NotAlone.Services;
using NotAlone.WebApp.Models;

namespace NotAlone.WebApp.ApiControllers
{
    [Route("api")]
    public class HomeApiController : Controller
    {
        private readonly ILoverHandlerService _loverHandlerService;

        private readonly IConfiguration _configuration;
        
        private readonly ILogger<HomeApiController> _logger;
        
        private readonly IVkService _vkService;

        public HomeApiController(
            ILoverHandlerService loverHandlerService,
            IConfiguration configuration,
            IVkService vkService,
            ILogger<HomeApiController> logger
        )
        {
            _loverHandlerService = loverHandlerService;
            _configuration = configuration;
            _logger = logger;
            _vkService = vkService;
        }

        [HttpPost]
        [Route("vk/callback")]
        public IActionResult Callback([FromBody] JsonElement request)
        {
            var result = _vkService.HandleRequest(request);
            return Ok(result);
        }
    }
}