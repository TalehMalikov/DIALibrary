using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Library.Core.Domain.Dtos;
using Library.Core.Utils;
using Library.Entities.Dtos;

namespace Library.Admin.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IAccountService _accountService;

        public AuthenticationController(IAuthService authService, IAccountService accountService)
        {
            _authService = authService;
            _accountService = accountService;
        }

        #region Login
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
                var role = GetRole(result.Data.Token);
                if (role == "SuperAdmin" || role == "Admin" || role == "GroupAdmin" || role == "ResourceAdmin")
                {
                    HttpContext.Session.SetString("AdminAccessToken", result.Data.Token);
                    HttpContext.Session.SetString("AdminEmail", result.Data.Email);
                    HttpContext.Session.SetString("UserRole", role);
                    HttpContext.Session.SetString("FullName", result.Data.FullName);
                    var account = await _accountService.GetByEmail(result.Data.Email);
                    //HttpContext.Session.SetString("AdminId", account.Data.Id.ToString());
                    return RedirectToAction("Index", "Home");
                }
                return RedirectToAction("Login");
            }
            return RedirectToAction("Login");
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


        #endregion

        #region ResetPassword

        public IActionResult ResetPassword()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (string.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePassword(AuthViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (string.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            var account = await _authService.GetAccountByAccountName(token, new ResetPasswordDto
            {
                AccountName = model.AccountName
            });
            if (account.Success)
            {
                var viewModel = new AuthViewModel();
                viewModel.Account = account.Data;
                return View(viewModel);
            }

            return RedirectToAction("ResetPassword");
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(AuthViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (string.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            if (!string.IsNullOrWhiteSpace(model.Password))
            {

                var result = await _accountService.Update(token, new AccountDto
                {
                    Id = model.Account.Id,
                    AccountName = model.Account.AccountName,
                    Email = model.Account.Email,
                    IsDeleted = model.Account.IsDeleted,
                    LastModified = DateTime.Now,
                    UserId = model.Account.User.Id,
                    PasswordHash = SecurityUtil.ComputeSha256Hash(model.Password.Trim())
                });
                if (result.Success)
                    return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("ResetPassword");
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (string.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(AuthViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (string.IsNullOrWhiteSpace(token)) return RedirectToAction("Login", "Authentication");
            string email = HttpContext.Session.GetString("AdminEmail");
            if (!(string.IsNullOrWhiteSpace(model.Password) & string.IsNullOrWhiteSpace(model.RepeatPassword)) &
                (model.Password == model.RepeatPassword))
            {

            }
            return View();
        }

        #endregion




    }
}
