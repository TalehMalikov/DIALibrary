using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class FacultyController : Controller
    {
        private readonly IFacultyService _facultyService;

        public FacultyController(IFacultyService facultyService)
        {
            _facultyService = facultyService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var result = await _facultyService.GetAll(token);
            var model = new FacultyViewModel
            {
                Faculties = result.Data
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(FacultyViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var result = await _facultyService.Add(token, model.Faculty);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var result = await _facultyService.Delete(token, id);
            return RedirectToAction("Index");
        }
    }
}
