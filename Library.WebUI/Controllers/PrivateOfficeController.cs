using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Library.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.WebUI.Controllers
{
    public class PrivateOfficeController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IStudentService _studentService;
        private readonly IFacultyService _faultyService;
        private readonly ISpecialtyService _specialtyService;
        private readonly ISectorService _sectorService;
        private string _email;

        public PrivateOfficeController(IAccountService accountService,IStudentService studentService,IFacultyService facultyService,ISectorService sectorService,
                                        ISpecialtyService specialtyService)
        {
            _accountService = accountService;
            _studentService = studentService;
            _faultyService = facultyService;
            _sectorService = sectorService;
            _specialtyService = specialtyService;
        }

        public async Task<IActionResult> Index()
        {
            _email = HttpContext.Session.GetString("Email");
            AccountViewModel viewModel = new AccountViewModel();
            var account = await _accountService.GetByEmail(_email);
            viewModel.Account = account.Data;

            var student = await _studentService.GetUserById(account.Data.User.Id);
            if(student.Success)
            {
                viewModel.Student = student.Data;
            }
            var faculties = await _faultyService.GetAllFaculties();
            viewModel.FacultySelectList = new SelectList(faculties.Data, "Id", "Name");

            var sectors = await _sectorService.GetAllSectors();
            viewModel.SectorSelectList = new SelectList(sectors.Data, "Id", "Name");

            var specialties = await _specialtyService.GetAllSpecialties();
            viewModel.SpecialtySelectList = new SelectList(specialties.Data, "Id", "Name");

            return View(viewModel);
        }

        public async Task<IActionResult> UpdateDetails(AccountViewModel model)
        {
            var result = await _accountService.Update(model.Account);

            if(model.Student!=null)
            {
                //var specialty = await _specialtyService.GetById(model.SelectedSpecialty.Id);
                //model.Student.Specialty = specialty.Data;


                /*var group = await _groupService.GetById(model.SelectedGroup.Id);
                model.Student.Group = group.Data;*/

                /*var faculty = await _faultyService.GetById(model.SelectedFaculty.Id);
                model.Student.Specialty.Faculty = faculty.Data;*/

                /*var sector = await _sectorService.GetById(model.SelectedSector.Id);
                model.Student.Group.Sector = sector.Data;*/

                _studentService.Update(model.Student);
            }

            return RedirectToAction("Index", "PrivateOffice");
        }
    }
}
