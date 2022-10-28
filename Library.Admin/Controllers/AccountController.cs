using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Core.Utils;
using Library.Entities.Dtos;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Text.Json;

namespace Library.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;

        public AccountController(IAccountService accountService, IUserService userService)
        {
            _accountService = accountService;
            _userService = userService;
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

        [HttpGet]
        public async Task<IActionResult> AccountDetails(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                AccountViewModel model = new AccountViewModel();
                var account = await _accountService.Get(token, id);
                
                model.Account = new AccountDto()
                {
                    Id=account.Data.Id,
                    UserId = account.Data.User.Id,
                    AccountName = account.Data.AccountName,
                    PasswordHash = account.Data.PasswordHash,
                    Email = account.Data.Email,
                    IsDeleted = account.Data.IsDeleted,
                    LastModified = account.Data.LastModified
                };
                List<Account> accounts = new List<Account>();
                accounts.Add(account.Data);
                foreach (var ac in accounts)
                {
                    model.AccountList.Add(new SelectListItem($"{ac.User.FirstName} {ac.User.LastName} {ac.User.FatherName}", $"{ac.User.Id}"));
                }
                return View(model);
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> SaveAccount(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if (accessToken != null)
            {
                AccountViewModel viewModel = new AccountViewModel();
                var account = await _accountService.Get(accessToken, id);
                if (account.Success == false)
                    return new NotFoundObjectResult(account.Message);
                viewModel.Account = new AccountDto()
                {
                    Id=account.Data.Id,
                    UserId = account.Data.User.Id,
                    AccountName = account.Data.AccountName,
                    PasswordHash = account.Data.PasswordHash,
                    Email = account.Data.Email,
                    IsDeleted = account.Data.IsDeleted,
                    LastModified = account.Data.LastModified
                };
                var accounts = await _userService.GetAll(accessToken);
                //var roles = await _roleService.GetAll(accessToken);

                foreach (var ac in accounts.Data)
                {
                    viewModel.AccountList.Add(new SelectListItem($"{ac.FirstName} {ac.LastName} {ac.FatherName}", $"{ac.Id}"));
                }

                return PartialView(viewModel);
            }
            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAccount(AccountViewModel viewModel)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if(accessToken!=null)
            {
                var result = await _accountService.Update(accessToken, viewModel.Account);
                if(result.Success)
                    return RedirectToAction("AccountDetails", "Account", $"{viewModel.Account.Id}");
                else
                    return BadRequest(result);
            }
            return new NotFoundResult();
        }

    }
}
