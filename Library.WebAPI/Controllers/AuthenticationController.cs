using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Business.Abstraction;
using Library.Core.Utils;
using Library.Entities.Concrete;
using Library.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<Account> userManager;
        private readonly SignInManager<Account> signInManager;
        private readonly IAccountService _accountService;
        private readonly IUserService _userService;
        public AuthenticationController(UserManager<Account> userManager, SignInManager<Account> signInManager, IAccountService accountService, IUserService userService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
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

            var signInResult = _signInManager.PasswordSignInAsync(account, model.Password, true, false).Result;

            if (signInResult.Succeeded == false)
                return BadRequest("Username or password is incorrect");

            string token = GenerateJwtToken(account);

            return Ok(new LoginResponseModel
            {
                Email = account.Email,
                Token = token
            });
        }


        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequestModel model)
        {
            var _user = _userService.Get(model.UserId);
            if (_user == null)
                return BadRequest("No user found with given id");

            var result = _accountService.Add(new Account
            {
                User = _user.Data,
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
                Expires = DateTime.Now.AddDays(15), //Token expires after 15 days
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #endregion
    }
}
