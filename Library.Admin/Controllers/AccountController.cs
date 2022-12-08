using Library.Admin.Models;
using Library.Admin.Services.Abstract;
using Library.Core.Utils;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace Library.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private readonly IAccountRoleService _accountRoleService;
        private readonly IStudentService _studentService;
        private readonly ISpecialtyService _specialtyService;
        private readonly IGroupService _groupService;
        public AccountController(IAccountService accountService, IUserService userService,IAccountRoleService accountRoleService,
                                IStudentService studentService, ISpecialtyService specialtyService, IGroupService groupService)
        {
            _accountService = accountService;
            _userService = userService;
            _accountRoleService = accountRoleService;
            _studentService = studentService;
            _specialtyService = specialtyService;
            _groupService = groupService;
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

        public async Task<IActionResult> SaveAccount(int id)
        {
            string token = HttpContext.Session.GetString("AdminAccessToken");
            return View();
        }

        public async Task<IActionResult> UpdateCommonAccount(int id)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if (accessToken != null)
            {
                AccountViewModel viewModel = new AccountViewModel();
                var account = await _accountService.Get(accessToken, id);

                if (account.Success == false)
                    return new NotFoundObjectResult(account.Message);

                viewModel.AccountRole = new AccountRole()
                {
                    Account = account.Data
                };

                if (account.Data.User.IsStudent)
                {
                    var student = await _studentService.GetByUserId(accessToken, account.Data.User.Id);
                    
                    viewModel.StudentDto = new StudentDto()
                    {
                        Id = student.Data.Id,
                        UserId = account.Data.User.Id,
                        SpecialtyId = student.Data.Specialty.Id,
                        GroupId = student.Data.Group.Id,
                        AcceptanceDate = student.Data.AcceptanceDate
                    };
                }

                var specialties = await _specialtyService.GetAll(accessToken);
                viewModel.SpecialtyList = new SelectList(specialties.Data, "Id", "Name");

                var groups = await _groupService.GetAll(accessToken);
                viewModel.GroupList = new SelectList(groups.Data, "Id", "Name");

                return PartialView(viewModel);
            }
            return new NotFoundResult();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCommonAccount(AccountViewModel viewModel)
        {
            string accessToken = HttpContext.Session.GetString("AdminAccessToken");
            if (accessToken != null)
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

                var userUpdate = await _userService.Update(accessToken, viewModel.AccountRole.Account.User);

                var updateAccount = await _accountService.Update(accessToken, accountDto);

                if(updateAccount.Success && userUpdate.Success)
                {
                    if(viewModel.AccountRole.Account.User.IsStudent)
                    {
                        var updateStudent = await _studentService.Update(accessToken, viewModel.StudentDto);
                        if (updateAccount.Success) 
                            return RedirectToAction("Index", "Account");
                        return BadRequest(updateStudent);
                    }
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    // log
                }
            }
            return new NotFoundResult();
        }
    }
}
