using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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

        public HomeApiController(
            ILoverHandlerService loverHandlerService,
            IConfiguration configuration,
            ILogger<HomeApiController> logger
        )
        {
            _loverHandlerService = loverHandlerService;
            _configuration = configuration;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult Index([FromBody] LoverPeopleRequestModel loverPeopleRequestModel)
        {
            _loverHandlerService.HandlePeople(loverPeopleRequestModel.FirstLoverPeopleModel,
                loverPeopleRequestModel.SecondLoverPeopleModel);
            return Ok();
        }

        [HttpPost]
        [Route("vk/callback")]
        public IActionResult Callback([FromBody] VkApiRequest request)
        {
            if (request != null)
            {
                _logger.LogInformation("VkApi:Confirmation: " + _configuration["VkApi:Confirmation"]);
                switch (request?.Type)
                {
                    case "confirmation":
                        return Ok(_configuration["VkApi:Confirmation"]);
                }
            }
            else
            {
                _logger.LogInformation("Empty request");
            }

            return Ok("ok");
        }
    }
}