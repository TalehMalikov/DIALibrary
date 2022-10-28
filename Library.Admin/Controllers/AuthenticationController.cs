using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Library.Admin.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;

        public AuthenticationController(IAuthService authService,IAccountService accountService)
        {
            _authService = authService;
            _accountService = accountService;
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
                HttpContext.Session.SetString("AdminAccessToken", result.Data.Token);
                HttpContext.Session.SetString("AdminEmail", result.Data.Email);
                HttpContext.Session.SetString("UserRole", GetRole(result.Data.Token));

                var account = await _accountService.GetByEmail(result.Data.Email);
                HttpContext.Session.SetString("AdminId",account.Data.Id.ToString());
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        protected string GetRole(string token)
        {
            string secret = "xecretKeywqejane";
            var key = Encoding.ASCII.GetBytes(secret);
            var handler = new JwtSecurityTokenHandler();
            var validations = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
            var claims = handler.ValidateToken(token, validations, out var tokenSecure);
            string role = ((JwtSecurityToken)tokenSecure).Payload["role"].ToString();
            return role;
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
