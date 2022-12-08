using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class HomeController : Controller
    {
        private readonly IFileService _fileService;
        private readonly IEducationalProgramService _educationalProgramService;
        private readonly IAccountRoleService _roleService;

        public HomeController(IFileService fileService, IEducationalProgramService educationalProgramService, IAccountRoleService roleService)
        {
            _fileService = fileService;
            _educationalProgramService = educationalProgramService;
            _roleService = roleService;
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (String.IsNullOrWhiteSpace(token))
                return RedirectToAction("Login", "Authentication");
            var educationalPrograms = await _educationalProgramService.GetAll(token);
            var accountRoles = await _roleService.GetAccountRoles(token);
            var files = await _fileService.GetAll();
            var model = new HomeViewModel
            {
                EducationalPrograms = educationalPrograms.Data.Count,
                TotalStudents = accountRoles.Data.Count(p => p.Account.User.IsStudent == true),
                TotalUsers = accountRoles.Data.Count,
                TotalAdmins = accountRoles.Data.Count(p => p.Role.Name.ToLower() != "user"),
                OurPublications = files.Data.Count(p => p.EditionStatus),
                TotalResources = files.Data.Count,
                TextBooks = files.Data.Count(p => p.FileType.Id == 5),
                TotalJournals = files.Data.Count(p => p.FileType.Id == 3),
                Abstracts = files.Data.Count(p => p.FileType.Id == 4),
                ElectronicArticles = files.Data.Count(p => p.FileType.Id == 2 && p.ExistingStatus == false),
                ElectronicBooks = files.Data.Count(p => p.FileType.Id == 1 && p.ExistingStatus == false),
                PhysicalArticles = files.Data.Count(p => p.FileType.Id == 2 && p.ExistingStatus == true),
                PhysicalBooks = files.Data.Count(p => p.ExistingStatus && p.FileType.Id == 1)
            };
            return View(model);
        }
    }
}
