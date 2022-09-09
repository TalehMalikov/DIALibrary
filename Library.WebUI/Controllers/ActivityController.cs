using Library.WebUI.Services.Abstract;
using Library.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class ActivityController : Controller
    {
        private readonly IActivityService _activityService;
        private readonly IFileTypeService _fileTypeService;

        public ActivityController(IActivityService activityService,IFileTypeService fileTypeService)
        {
            _activityService = activityService;
            _fileTypeService = fileTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var allFileTypes = await _fileTypeService.GetAllFileTypes();
            ViewBag.AllFileTypes = allFileTypes.Data;

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
