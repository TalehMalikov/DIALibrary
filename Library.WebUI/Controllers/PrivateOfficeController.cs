using Library.WebUI.Services.Abstract;
using Library.WebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace Library.WebUI.Controllers
{
    public class PrivateOfficeController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IStudentService _studentService;
        private readonly IFacultyService _faultyService;
        private readonly ISpecialtyService _specialtyService;
        private readonly ISectorService _sectorService;
        private readonly IUserService _userService;
        private readonly IFileTypeService _fileTypeService;

        public PrivateOfficeController(IAccountService accountService,IStudentService studentService,IFacultyService facultyService,ISectorService sectorService,
                                        ISpecialtyService specialtyService, IUserService userService,IFileTypeService fileTypeService)
        {
            _accountService = accountService;
            _studentService = studentService;
            _faultyService = facultyService;
            _sectorService = sectorService;
            _specialtyService = specialtyService;
            _userService = userService;
            _fileTypeService = fileTypeService;
        }

        public async Task<IActionResult> Index()
        {
            var email = HttpContext.Session.GetString("Email");
            AccountViewModel viewModel = new AccountViewModel();
            
            var accessToken = HttpContext.Session.GetString("AccessToken");


            var account = await _accountService.GetByAccountName( email);
            viewModel.Account = account.Data;

            var allFileTypes = await _fileTypeService.GetAllFileTypes();
            ViewBag.AllFileTypes = allFileTypes.Data;

            viewModel.SelectedGender = viewModel.Account.User.Gender;

            var student = await _studentService.GetUserById(accessToken,account.Data.User.Id);
            if(student.Success)
            {
                viewModel.Student = student.Data;
            }
            return View(viewModel);
        }

        public async Task<IActionResult> UpdateDetails(AccountViewModel model)
        {
            var accessToken = HttpContext.Session.GetString("AccessToken");

            model.Account.LastModified = DateTime.Now;
            model.Account.User.LastModified = DateTime.Now;
            model.Account.User.Gender = model.SelectedGender;
            await _accountService.Update(accessToken , model.Account);

            await _userService.Update(accessToken , model.Account.User);

            if(model.Student!=null)
            {
                await _studentService.Update(accessToken , model.Student);
            }
            return RedirectToAction("Index", "PrivateOffice");
        }

        public IActionResult LogOut()
        {
            HttpContext.Session.Clear();

            Response.Clear();

            Response.Cookies.Delete(".AspNetCore.Session");

            return RedirectToAction("Index", "Home");
        }
    }
}
