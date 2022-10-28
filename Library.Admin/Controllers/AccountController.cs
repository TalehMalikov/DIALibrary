using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Core.Utils;
using Library.Entities.Dtos;
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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewAccount(AccountViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if(token!=null)
            {
                model.Password = model.Password.Trim();
                model.RepeatPassword = model.RepeatPassword.Trim();
                if ((String.IsNullOrWhiteSpace(model.Password) && String.IsNullOrWhiteSpace(model.RepeatPassword)) ||
                    model.Password != model.RepeatPassword)
                    return View();
                model.AccountDto.PasswordHash = SecurityUtil.ComputeSha256Hash(model.Password);
                await _accountService.Add(token, model.AccountDto);
                return View();
            }
            return new NotFoundResult();
        }

        public async Task<IActionResult> Index()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if(token!=null)
            {
                var result = await _accountService.GetAll(token);
                var model = new AccountViewModel
                {
                    Accounts = result.Data
                };
                return View(model);
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> SaveAccount(int id)
        {
            var model = new AccountViewModel();
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var account = await _accountService.Get(token, id);
            model.Account = account.Data;
            return PartialView(model);
        }

        //[HttpPut]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SaveAccount(AccountViewModel model)
        //{
        //    string token = HttpContext.Session.GetString("AdminAccessToken");
            
        //}
    }
}
