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
            return View(viewModel);
        }


        //***********************************

        public async Task<IActionResult> GetAllPlayers()
        {
            var strToken = HttpContext.Session.GetString("AccessToken");

            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:42045/api/player");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", strToken);

            using HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var apiString = await response.Content.ReadAsStringAsync();
                //players = JsonConvert.DeserializeObject<List<Player>>(apiString);

            }
            return View(/*players*/);
        }
    }
}
