using Library.WebUI.Services.Abstract;
using Library.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;

        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        public async Task<IActionResult> Index()
        {
            var model = new ActivityViewModel
            {
                Activities = await _activityService.GetAll()
            };
            return View(model);
        }

        public async Task<IActionResult> Show(int id)
        {
            var model = new ActivityViewModel
            {
                Activity = await _activityService.Get(id)
            };
            return View(model);
        }
    }
}
