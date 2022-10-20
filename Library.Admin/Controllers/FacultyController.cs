using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using NuGet.Common;

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
            if (token != null)
            {
                var result = await _facultyService.GetAll(token);
                var model = new FacultyViewModel
                {
                    Faculties = result.Data
                };

                return View(model);
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> SaveFaculty(int id)
        {
            var model = new FacultyViewModel();
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (id == 0)
                return PartialView();
            var faculty = await _facultyService.Get(token, id);
            model.Faculty = faculty.Data;
            return PartialView(model);
        }


        [HttpPost]
        public async Task<IActionResult> SaveFaculty(FacultyViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (model.Faculty.Id == 0)
            {
                await _facultyService.Add(token, model.Faculty);
                return RedirectToAction("Index");
            }

            await _facultyService.Update(token, model.Faculty);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                var result = await _facultyService.Delete(token, id);
                return RedirectToAction("Index");
            }
            return new NotFoundResult();
        }
    }
}
