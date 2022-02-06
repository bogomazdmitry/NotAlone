using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NotAlone.Models;
using NotAlone.Services;
using NotAlone.WebApp.Models;

namespace NotAlone.WebApp.ApiControllers
{
    [Route("api")]
    public class HomeApiController: Controller
    {
        private readonly ILoverHandlerService _loverHandlerService;

        private readonly IConfiguration _configuration;
        
        public HomeApiController(ILoverHandlerService loverHandlerService, IConfiguration configuration)
        {
            _loverHandlerService = loverHandlerService;
            _configuration = configuration;
        }
        
        [HttpPost]
        public IActionResult Index([FromBody] LoverPeopleRequestModel loverPeopleRequestModel)
        {
            _loverHandlerService.HandlePeople(loverPeopleRequestModel.FirstLoverPeopleModel, loverPeopleRequestModel.SecondLoverPeopleModel);
            return Ok();
        }
        
        [HttpPost]
        [Route("vk/callback")]
        public IActionResult Callback([FromBody] VkApiRequest request)
        {
            switch (request.Type)
            {
                case "confirmation":
                    return Ok(_configuration["VkApi:Confirmation"]);
            }
            return Ok("ok");
        }
    }
}