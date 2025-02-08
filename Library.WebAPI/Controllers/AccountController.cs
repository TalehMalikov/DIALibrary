using Library.Business.Abstraction;
using Library.Core.Domain.Dtos;
using Library.Entities.Concrete;
using Library.Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Library.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            this._accountService = accountService;
        }

        [HttpPut]
        [Route("update")]
        [Authorize]
        public IActionResult Update(AccountDto account)
        {
            var result = _accountService.Update(account);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet]
        [Route("getall")]
        [Authorize]
        public IActionResult GetAll()
        {
            var result = _accountService.GetAll();

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getbyid/{id:int}")]
        [Authorize]
        public IActionResult Get(int id)
        {
            var result = _accountService.Get(id);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpGet("getbyemail")]
        public IActionResult GetByEmail(string name)
        {
            var result = _accountService.GetByEmail(name);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpDelete("{id:int}")]
        [Authorize(Roles="SuperAdmin,Admin,GroupAdmin")]
        public IActionResult Delete(int id)
        {
            var result = _accountService.Delete(id);

            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }

        [HttpPut("resetpassword")]
        public IActionResult ResetPassword(AccountDto account)
        {
            var result = _accountService.Update(account);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("add")]
        public IActionResult Add(AccountDto account)
        {
            var result = _accountService.Add(account);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpPost("getbyaccountname")]
        [Authorize(Roles = "User,GroupAdmin,ResourceAdmin,Admin,SuperAdmin")]
        public IActionResult GetByAccountName(ResetPasswordDto value)
        {
            var result = _accountService.GetByAccountName(value.AccountName);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
    }
}
