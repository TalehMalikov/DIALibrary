using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;
        private readonly IUserService _userService;
        public StudentController(IStudentService studentService, IUserService userService)
        {
            _studentService = studentService;
            _userService = userService;
        }

        public async Task<IActionResult> ShowStudents()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                var result = await _studentService.GetAll(token);
                var model = new StudentViewModel
                {
                    Students = result.Data
                };
                return View(model);
            }
            return new NotFoundResult();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var responseStudent = await _studentService.Delete(token, id);
            var responseUser = await _userService.Delete(token, id);
            return RedirectToAction("ShowStudents");
        }
    }
}
