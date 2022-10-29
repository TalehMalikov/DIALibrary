using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Core.Utils;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
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
        private readonly IAccountRoleService _accountRoleService;

        public AccountController(IAccountService accountService, IUserService userService,IAccountRoleService accountRoleService)
        {
            _accountService = accountService;
            _userService = userService;
            _accountRoleService = accountRoleService;
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
        public async Task<IActionResult> AccountDetails(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (token != null)
            {
                AccountViewModel model = new AccountViewModel();
                var account = await _accountRoleService.GetAccountRoleByAccountId(token, id);
                model.AccountRole = account.Data;
                return View(model);
            }
            return new NotFoundResult();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAccount(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if (accessToken != null)
            {
                AccountViewModel viewModel = new AccountViewModel();
                var account = await _accountService.Get(accessToken, id);
                if (account.Success == false)
                    return new NotFoundObjectResult(account.Message);
                var accountRole = await _accountRoleService.GetAccountRoleByAccountId(accessToken, id);
                viewModel.AccountRole = accountRole.Data;

                var roles = await _accountRoleService.GetRoles(accessToken);
                viewModel.Roles = new SelectList(roles.Data,"Id","Name");

                return PartialView(viewModel);
            }
            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAccount(AccountViewModel viewModel)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if(accessToken!=null)
            {
                var updateUser = await _userService.Update(accessToken,viewModel.AccountRole.Account.User);
                if(updateUser.Success)
                {
                    var accountRoleDto = new AccountRoleDto()
                    {
                        Id=viewModel.AccountRole.Id,
                        RoleId = viewModel.AccountRole.Role.Id,
                        AccountId = viewModel.AccountRole.Account.Id
                    };
                    var updateAccountRole = await _accountRoleService.Update(accessToken, accountRoleDto);
                    if(updateAccountRole.Success)
                    {
                        var accountDto = new AccountDto()
                        {
                            Id = viewModel.AccountRole.Account.Id,
                            UserId = viewModel.AccountRole.Account.User.Id,
                            AccountName = viewModel.AccountRole.Account.AccountName,
                            PasswordHash = viewModel.AccountRole.Account.PasswordHash,
                            Email = viewModel.AccountRole.Account.Email,
                            IsDeleted = viewModel.AccountRole.Account.IsDeleted,
                            LastModified = viewModel.AccountRole.Account.LastModified
                        };
                        var result = await _accountService.Update(accessToken, accountDto);
                        if (result.Success)
                            return RedirectToAction("AccountDetails", "Account", new { id = $"{viewModel.AccountRole.Account.Id}" });
                        else
                            return BadRequest(result);
                    }
                    else
                        return BadRequest(updateAccountRole);
                }
                return BadRequest(updateUser.Message);
            }
            return new NotFoundResult();
        }
    }
}
