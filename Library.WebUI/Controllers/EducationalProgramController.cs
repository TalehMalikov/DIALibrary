using Library.WebUI.Services.Abstract;
using Library.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class EducationalProgramController : Controller
    {
        private readonly IEducationalProgramService _educationalProgramService;

        public EducationalProgramController(IEducationalProgramService educationalProgramService)
        {
            _educationalProgramService = educationalProgramService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AccessToken");
            var result = await _educationalProgramService.GetAll(token);
            var model = new EducationalProgramViewModel
            {
                EducationalPrograms = result.Data
            };
            return View(model);
        }
    }
}
