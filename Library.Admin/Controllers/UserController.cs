using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IGroupService _groupService;

        public UserController(IUserService userService, IStudentService studentService, ISpecialtyService specialtyService, IGroupService groupService)
        {
            _userService = userService;
            _studentService = studentService;
            _specialtyService = specialtyService;
            _groupService = groupService;
        }
        [HttpGet]
        public async Task<IActionResult> NewUser()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var model = new UserViewModel();
            var specialties = await _specialtyService.GetAll(token);
            var groups = await _groupService.GetAll(token);
            var group = new Group
            {
                Id = 0,
                Name = "Qrup seçin"
            };
            groups.Data.Insert(0,group);
            specialties.Data.Insert(0, new Specialty { Id = 0, Name = "İxtisas seçin" });
            model.GroupList = new SelectList(groups.Data, "Id", "Name", group);
            model.SpecialtyList = new SelectList(specialties.Data, "Id", "Name",
                new Specialty { Id = 0, Name = "İxtisas seçin" });
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> NewUser(UserViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (model.Student.Specialty.Id != 0 & model.Student.Group.Id != 0)
            {
                var userId = await _userService.AddAsStudent(model.User,token);
                model.Student.User = new User { Id = userId.Data };
                var result = _studentService.Add(token, model.Student);
            }
            else
                 await _userService.Add(token, model.User);

            return RedirectToAction("NewUser");
        }

        public async Task<IActionResult> ShowUsers()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var result = await _userService.GetAll(token);
            var model = new UserViewModel
            {
                Users = result.Data
            };
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var result = await _userService.Delete(token,id);
            if(result.Success)
                return RedirectToAction("ShowUsers");
            return RedirectToAction("Index", "Home");
        }
    }
}
