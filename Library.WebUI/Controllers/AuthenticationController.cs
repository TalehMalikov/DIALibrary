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
        public AuthenticationController(IAuthService authService)
        {
            _authService = authService;
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
            }
            // hesab adı və ya parol səhvdir mesajini modala göndər
            return RedirectToAction("Index", "Home");
        }
    }
}
