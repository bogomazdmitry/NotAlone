using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NotAlone.Models;
using NotAlone.Services;

namespace NotAlone.WebApp.Controllers
{
    public class QueueController : Controller
    {
        private readonly ILoverHandlerService _loverHandleService;
        
        public QueueController(ILoverHandlerService loverHandlerService)
        {
            _loverHandleService = loverHandlerService;
        }
        
        public async Task<IActionResult> Index()
        {
            var queueItems = await _loverHandleService.GetQueueItems();
            return View(queueItems);
        }

        public async Task<IActionResult> HandleAllQueueItems()
        {
            (await _loverHandleService.GetQueueItems())
                .AsParallel()
                .ForAll(e =>
                {
                    _loverHandleService.HandlePeople(e.FirstPerson, e.SecondPerson, e.IsBlindDate, e.LinkBlindDate);
                    _loverHandleService.RemoveQueueItem(e);
                });
            return Redirect("Index");
        }

        public async Task<IActionResult> DeleteQueueItem(string firstPersonVkUrl, string secondPersonVkUrl)
        {
            await _loverHandleService.RemoveQueueItem(firstPersonVkUrl, secondPersonVkUrl);
            return Redirect("Index");
        }
    }
}
