using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Library.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<Account> userManager;
        private readonly SignInManager<Account> signInManager;
        private readonly IAccountService accountService;
        public AccountController(UserManager<Account> userManager, SignInManager<Account> signInManager,IAccountService accountService)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.accountService = accountService;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequestModel model)
        {
            var account = userManager.FindByNameAsync(model.Email).Result;

            if (account == null)
                return BadRequest("Username or password is incorrect");

            var signInResult = signInManager.PasswordSignInAsync(account, model.Password, true, false).Result;

            if (signInResult.Succeeded == false)
                return BadRequest("Username or password is incorrect");

            string token = GenerateJwtToken(account);

            return Ok(new LoginResponseModel()
            {
                Email = account.Email,
                Token = token
            });
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(Account account)
        {

            try
            {
                accountService.Add(account);

                return Ok("Success!");
            }
            catch
            {
                return BadRequest("Failed to add");
            }
        }

        [HttpPut]
        [Route("update")]
        public IActionResult Update(Account account)
        {

                var accountToUpdate = accountService.Get(account.Id);

                if (accountToUpdate == null)
                    return NotFound($"Account with Id = {account.Id} not found");

                 var result  = accountService.Update(account);
                 if(result.Success) 
                        return Ok("Successfully Updated");
                 return BadRequest(result);
        }

        [HttpGet]
        [Route("getall")]
        public IActionResult GetAll()
        {
            try
            {
                var accounts = accountService.GetAll();

                if (accounts == null)
                    return BadRequest("No Account found with given id");

                return Ok(accounts);
            }
            catch
            {
                return BadRequest("Unknown error occured");
            }
        }

        [HttpGet("getbyid/{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var account = accountService.Get(id);

                if (account == null)
                    return BadRequest("No Account found with given id");

                return Ok(account);
            }
            catch
            {
                return BadRequest("Unknown error occured");
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var account = accountService.Get(id);

                if (account == null)
                    return BadRequest("No such a account found to delete");

                accountService.Delete(id);

                return Ok("Successfully deleted");
            }
            catch
            {
                return BadRequest("Failed to delete");
            }
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
