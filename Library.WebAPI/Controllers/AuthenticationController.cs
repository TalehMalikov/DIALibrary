using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Business.Abstraction;
using Library.Core.Domain.Dtos;
using Library.Core.Result.Concrete;
using Library.Core.Utils;
using Library.Entities.Concrete;
using Library.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Account> _userManager;
        private readonly SignInManager<Account> _signInManager;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        private List<string> _roles = new List<string>(2);
        public AuthenticationController(UserManager<Account> userManager, SignInManager<Account> signInManager, IAccountService accountService, IUserService userService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _userService = userService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequestModel model)
        {
            var account = _userManager.FindByNameAsync(model.Email).Result;

            if (account == null)
                return BadRequest("Username or password is incorrect");

            var roles = _accountService.GetRoles(account).Data.ToList();

            foreach (var role in roles)
            {
                _roles.Add(role.Name);
            }

            var signInResult = _signInManager.PasswordSignInAsync(account, model.Password, true, false).Result;

            _userManager.AddToRolesAsync(account, _roles);

            if (signInResult.Succeeded == false)
                return BadRequest("Username or password is incorrect");

            string token = GenerateJwtToken(account);
            return Ok(new SuccessDataResult<LoginResponseDto>(new LoginResponseDto
            {
                Email = account.Email,
                Token = token
            }));
            
        }

        [HttpPost]
        [Route("register")]
        [Authorize("SuperAdmin,Admin,GroupAdmin")]
        public IActionResult Register(RegisterRequestModel model)
        {
            var user = _userService.Get(model.UserId);
            if (user?.Data == null)
                return BadRequest("No user found with given id");

            var result = _accountService.Add(new Account
            {
                User = user.Data,
                Email = model.Email,
                LastModified = DateTime.Now,
                IsDeleted = false,
                AccountName = model.AccountName,
                PasswordHash = SecurityUtil.ComputeSha256Hash(model.Password),
            });

            if (result.Success)
                return Ok(result);
            return BadRequest(result);

        }

        #region private logic

        private string GenerateJwtToken(Account account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII.GetBytes("xecretKeywqejane");

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Name, account.Id.ToString()),
                    new Claim(ClaimTypes.Email, account.Email),
                }),
                Expires = DateTime.Now.AddHours(1.5D), //Token expires after 1.5 hours
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            foreach (var item in _roles)
            {
                tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, item));
            }

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
