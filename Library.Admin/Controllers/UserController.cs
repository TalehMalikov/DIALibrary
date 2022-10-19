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
            if(token!=null)
            {
                var model = new UserViewModel();
                var specialties = await _specialtyService.GetAll(token);
                var groups = await _groupService.GetAll(token);
                var group = new Group
                {
                    Id = 0,
                    Name = "Qrup seçin"
                };
                groups.Data.Insert(0, group);
                specialties.Data.Insert(0, new Specialty { Id = 0, Name = "İxtisas seçin" });
                model.GroupList = new SelectList(groups.Data, "Id", "Name", group);
                model.SpecialtyList = new SelectList(specialties.Data, "Id", "Name",
                    new Specialty { Id = 0, Name = "İxtisas seçin" });
                return View(model);
            }
            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> NewUser(UserViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if(token!=null)
            {
                if (model.Student.SpecialtyId != 0 & model.Student.GroupId != 0)
                {
                    var userId = await _userService.AddAsStudent(model.User, token);
                    model.Student.UserId = userId.Data;
                    var result = await _studentService.Add(token, model.Student);
                    if (result.Success)
                        return RedirectToAction("ShowStudents", "Student");
                    await _userService.DeleteFromDb(userId.Data, token);
                }
                else
                {
                    await _userService.Add(token, model.User);
                }
                return RedirectToAction("NewUser");
            }
            return new NotFoundResult();
        }

        public async Task<IActionResult> ShowUsers()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if(token!=null)
            {
                var result = await _userService.GetAll(token);
                var model = new UserViewModel
                {
                    Users = result.Data
                };
                return View(model);
            }
            return new NotFoundResult();
        }

        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if(token!=null)
            {
                var result = await _userService.Delete(token, id);
                if (result.Success)
                    return RedirectToAction("ShowUsers");
                return RedirectToAction("Index", "Home");
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> SaveUser(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var model = new UserViewModel();
            if (id == 0)
                return PartialView();
            var user = await _userService.Get(token,id);
            model.User = user.Data;
            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> SaveUser(UserViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (model.User.Id == 0)
            {
                await _userService.Add(token, model.User);
                return RedirectToAction("ShowUsers");
            }

            await _userService.Update(token, model.User);
            return RedirectToAction("ShowUsers");
        }

        [HttpGet]
        public async Task<IActionResult> DeactiveUsers()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var model = new UserViewModel();
            var result = await _userService.GetDeactiveUsers(token);
            model.Users = result.Data;
            return View(model);
        }

    }
}
