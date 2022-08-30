using Library.Core.Domain.Dtos;
using Library.WebUI.Models;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;

namespace Library.WebUI.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ICategoryService _categoryService;
        private readonly IFileTypeService _fileTypeService;

        public AuthenticationController(IAuthService authService, ICategoryService categoryService,IFileTypeService fileTypeService)
        {
            _authService = authService;
            _categoryService = categoryService;
            _fileTypeService = fileTypeService;
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
                HttpContext.Session.SetString("AccessToken",result.Data.Token);
                HttpContext.Session.SetString("Email",loginViewModel.LoginModel.Email);
                return RedirectToAction("Index", "Authentication");
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            AuthenticationViewModel viewModel = new AuthenticationViewModel();
            viewModel.NewAddedBookList = await _categoryService.GetNewAddedBooks();
            viewModel.AllFileTypes = await _fileTypeService.GetAllFileTypes();
            return View(viewModel);
        }
    }
}
