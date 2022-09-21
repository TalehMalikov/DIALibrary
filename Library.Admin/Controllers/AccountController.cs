using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Core.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Library.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public IActionResult NewAccount()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> NewAccount(AccountViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            model.Password = model.Password.Trim();
            model.RepeatPassword = model.RepeatPassword.Trim();
            if ((String.IsNullOrWhiteSpace(model.Password) && String.IsNullOrWhiteSpace(model.RepeatPassword)) ||
                model.Password != model.RepeatPassword) 
                return View();
            model.Account.PasswordHash = SecurityUtil.ComputeSha256Hash(model.Password);
            await _accountService.Add(token,model.Account);
            return View();
        }
    }
}
