using Microsoft.AspNetCore.Mvc;
using NotAlone.Services;
using NotAlone.WebApp.Models;

namespace NotAlone.WebApp.ApiControllers
{
    [Route("api")]
    public class HomeApiController: Controller
    {
        private readonly ILoverHandlerService _loverHandlerService;

        public HomeApiController(ILoverHandlerService loverHandlerService)
        {
            _loverHandlerService = loverHandlerService;
        }
        
        [HttpPost]
        public IActionResult Index([FromBody] LoverPeopleRequestModel loverPeopleRequestModel)
        {
            _loverHandlerService.HandlePeople(loverPeopleRequestModel.FirstLoverPeopleModel, loverPeopleRequestModel.SecondLoverPeopleModel);
            return Ok();
        }
    }
}