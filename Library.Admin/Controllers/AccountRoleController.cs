using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Admin.Controllers
{
    public class AccountRoleController : Controller
    {
        private readonly IAccountRoleService _roleService;
        private readonly IAccountService _accountService;

        public AccountRoleController(IAccountRoleService roleService,IAccountService accountService)
        {
            _roleService = roleService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> ShowRoles()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var result = await _roleService.GetRoles(token);
            var model = new AccountRoleViewModel
            {
                Roles = result.Data
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ShowRoleDescription(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var roles = await _roleService.GetRoles(token);
            var role = roles.Data.FirstOrDefault(x => x.Id == id);
            var model = new AccountRoleViewModel
            {
                Role = role
            };
            return PartialView(model);
        }

        [HttpGet]
        public async Task<IActionResult> ShowAccountRoles()
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var accountRoles = await _roleService.GetAccountRoles(token);
            var model = new AccountRoleViewModel
            {
                AccountRoles = accountRoles.Data
            };
            return View(model);
        }

        
        public async Task<IActionResult> Delete(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            await _roleService.Delete(token, id);
            return RedirectToAction("ShowAccountRoles");
        }

        [HttpGet]
        public async Task<IActionResult> SaveAccountRole(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            var roles = await _roleService.GetRoles(token);
            var model = new AccountRoleViewModel();
            model.RoleList = new SelectList(roles.Data, "Id", "Name");
            if (id == 0)
            {
                var accounts = await _accountService.GetAll(token);
                foreach (var ac in accounts.Data)
                    model.AccountList.Add(new SelectListItem($"{ac.User.FirstName} {ac.User.LastName} {ac.User.FatherName}", $"{ac.Id}"));
                return PartialView(model);
            }
            var accountRole = await _roleService.GetAccountRole(token, id);
            model.AccountRole = accountRole.Data;
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveAccountRole(AccountRoleViewModel model)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            if (model.AccountRole.Id == 0)
            { 
                await _roleService.Add(token, new AccountRoleDto
                {
                    AccountId = model.AccountRole.Account.Id,
                    RoleId = model.AccountRole.Role.Id
                });
                return RedirectToAction("ShowAccountRoles");
            }

            await _roleService.Update(token, new AccountRoleDto
            {
                Id = model.AccountRole.Id,
                RoleId = model.AccountRole.Role.Id,
                AccountId = model.AccountRole.Account.Id
            });
            return RedirectToAction("ShowAccountRoles");
        }
    }
}
