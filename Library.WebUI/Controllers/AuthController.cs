using Library.Core.Domain.Dtos;
using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ICategoryService _categoryService;

        public AuthController(IAuthService authService, ICategoryService categoryService)
        {
            _authService = authService;
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthViewModel loginViewModel)
        {
            var result = await  _authService.Login(new AccountLoginDto
            {
                Email = loginViewModel.LoginModel.Email,
                Password = loginViewModel.LoginModel.Password
            });
            if (result.Success)
                return RedirectToAction("Index","Auth");
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AuthViewModel authViewModel = new AuthViewModel();
            authViewModel.NewAddedBookList = await _categoryService.GetNewAddedBooks();
            return View(authViewModel);
        }
    }
}
