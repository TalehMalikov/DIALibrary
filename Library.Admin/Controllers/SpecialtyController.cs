using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Controllers
{
    public class SpecialtyController : Controller
    {
        private readonly ISpecialtyService _specialtyService;
        private readonly IFacultyService _faultyService;

        public SpecialtyController(ISpecialtyService specialtyService, IFacultyService faultyService)
        {
            _specialtyService = specialtyService;
            _faultyService = faultyService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var result = await _specialtyService.GetAll(token);
            var facultyList = await _faultyService.GetAll(token);
            var model = new SpecialtyViewModel
            {
                SpecialtyList = result.Data,
                FacultyList = new SelectList(facultyList.Data,"Id","Name")
            };
            return View(model);
        }

        public async Task<IActionResult> Add(SpecialtyViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (model.Specialty.FacultyId == 0 || String.IsNullOrWhiteSpace(model.Specialty.Name))
                return RedirectToAction("Index");
            await _specialtyService.Add(token, model.Specialty);
            return RedirectToAction("Index");
        }
    }
}
