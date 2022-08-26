using Library.Core.Domain.Dtos;
using Library.Entities.Concrete;
using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ICategoryService _categoryService;

        public AuthenticationController(IAuthService authService, ICategoryService categoryService)
        {
            _authService = authService;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticationViewModel loginViewModel)
        {
            var result = await _authService.Login(new AccountLoginDto
            {
                Email = loginViewModel.LoginModel.Email,
                Password = loginViewModel.LoginModel.Password
            });
            if (result.Success)
            {
                //Session["AccesToken"] = result.Data.Token;

                return RedirectToAction("Index", "Auth");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AuthenticationViewModel authViewModel = new AuthenticationViewModel();
            authViewModel.NewAddedBookList = await _categoryService.GetNewAddedBooks();
            return View(authViewModel);
        }
    }
}
