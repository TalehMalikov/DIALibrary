using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
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
            var result = await _userService.Add(model.User, token);
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
    }
}
