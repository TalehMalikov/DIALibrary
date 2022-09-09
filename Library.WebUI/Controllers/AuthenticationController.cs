using Library.Core.Domain.Dtos;
using Library.WebUI.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using Library.Core.Utils;
using Library.WebUI.ViewModels;

namespace Library.WebUI.Controllers
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

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(PasswordViewModel model)
        {
            var result = await _accountService.GetByEmail(model.Email);
            string email = result.Data.Email; 
            if (result.Success & model.Password == email)
            {
                string code = MailKitUtil.GenerateVerificationCode();
                HttpContext.Session.SetString("EmailToResetPassword",email);
                MailKitUtil.SendMail(email,"Verification code",code);
                HttpContext.Session.SetString("VerificationCode",code);
                return RedirectToAction("ConfirmVerificationCode");
            }
            return View();
        }

        public IActionResult ConfirmVerificationCode()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ConfirmVerificationCode(PasswordViewModel model)
        {
            string? code = HttpContext.Session.GetString("VerificationCode");
            if (code == model.VerificationCode)
            {    
                RedirectToAction("ResetPassword");
            }
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(PasswordViewModel model)
        {
            string? email = HttpContext.Session.GetString("EmailToResetPassword");
            if (String.IsNullOrWhiteSpace(HttpContext.Session.GetString("VerificationCode")))
                return RedirectToAction("ForgotPassword");
            if (model.Password != model.RepeatPassword)
                return View();
            var account = await _accountService.GetByEmail(email);
            account.Data.PasswordHash = SecurityUtil.ComputeSha256Hash(model.Password);
            await _accountService.ResetPassword(account.Data);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(PasswordViewModel model)
        {
            string? token = HttpContext.Session.GetString("AccessToken");
            string? email = HttpContext.Session.GetString("Email");
            var account = await _accountService.GetByEmail(email);
            if (SecurityUtil.ComputeSha256Hash(model.OldPassword) != account.Data.PasswordHash)
                return View();
            if (model.Password != model.RepeatPassword)
                return View();
            account.Data.PasswordHash = SecurityUtil.ComputeSha256Hash(model.Password);
            await _accountService.Update(token, account.Data);
            return RedirectToAction("Index","PrivateOffice");
        }
    }
}
