using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NuGet.Common;

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
            if (token != null)
            {
                var result = await _specialtyService.GetAll(token);
                var facultyList = await _faultyService.GetAll(token);
                var model = new SpecialtyViewModel
                {
                    SpecialtyList = result.Data,
                    FacultyList = new SelectList(facultyList.Data, "Id", "Name")
                };
                return View(model);
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> SaveSpecialty(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var model = new SpecialtyViewModel();
            var facultyList = await _faultyService.GetAll(token);
            model.FacultyList = new SelectList(facultyList.Data, "Id", "Name");
            if (id == 0)
                return PartialView(model);
            var specialty = await _specialtyService.Get(token, id);
            model.Specialty = specialty.Data;
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveSpecialty(SpecialtyViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");

            if (model.Specialty.Faculty.Id == 0 || String.IsNullOrWhiteSpace(model.Specialty.Name))
                return RedirectToAction("Index");
            if (model.Specialty.Id == 0)
            {
                await _specialtyService.Add(token, new SpecialtyDto
                {
                    FacultyId = model.Specialty.Faculty.Id,
                    Name = model.Specialty.Name
                });
                return RedirectToAction("Index");
            }
            await _specialtyService.Update(token, new SpecialtyDto
            {
                Id = model.Specialty.Id,
                FacultyId = model.Specialty.Faculty.Id,
                Name = model.Specialty.Name
            });
            return RedirectToAction("Index");
        }
    }
    
}
