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
        private readonly IUserService _userService;

        public PrivateOfficeController(IAccountService accountService,IStudentService studentService,IFacultyService facultyService,ISectorService sectorService,
                                        ISpecialtyService specialtyService, IUserService userService)
        {
            _accountService = accountService;
            _studentService = studentService;
            _faultyService = facultyService;
            _sectorService = sectorService;
            _specialtyService = specialtyService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var email = HttpContext.Session.GetString("Email");
            AccountViewModel viewModel = new AccountViewModel();

            var accessToken = HttpContext.Session.GetString("AccessToken");

            var account = await _accountService.GetByEmail(accessToken , email);
            viewModel.Account = account.Data;


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

            var updateAccount = await _accountService.Update(accessToken , model.Account);

            var updateUser = await _userService.Update(accessToken , model.Account.User);

            if(model.Student!=null)
            {
                _studentService?.Update(accessToken , model.Student);
            }
            return RedirectToAction("Index", "PrivateOffice");
        }
    }
}
