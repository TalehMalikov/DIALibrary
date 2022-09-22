using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IStudentService _studentService;

        public UserController(IUserService userService, IStudentService studentService)
        {
            _userService = userService;
            _studentService = studentService;
        }
        [HttpGet]
        public IActionResult NewUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewUser(UserViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (model.Student.Specialty.Id != 0 & model.Student.Group.Id != 0)
            {
                var userId = await _userService.AddAsStudent(model.User,token);
                model.Student.User.Id = userId.Data;
                var result = _studentService.Add(token, model.Student);
            }
            else
                 await _userService.Add(token, model.User);
            return View();
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
            await _userService.Delete(token,id);
            return RedirectToAction("ShowUsers");
        }
    }
}
