using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthViewModel model)
        {
            var result = await _authService.Login(model.AccountLoginDto);
            if (result.Success)
            {
                HttpContext.Session.SetString("AdminAccessToken",result.Data.Token);
                HttpContext.Session.SetString("AdminEmail",result.Data.Email);
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();

            Response.Clear();

            Response.Cookies.Delete(".AspNetCore.Session");

            return RedirectToAction("Login");
        }
    }
}
