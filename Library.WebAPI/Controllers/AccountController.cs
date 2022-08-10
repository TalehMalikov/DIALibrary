using Library.Business.Abstraction;
using Library.Entities.Concrete;
using Library.WebAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Library.Core.Utils;
using Microsoft.AspNetCore.Authorization;

namespace Library.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   // [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;
        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
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
                //var account = accountService.Get(id);

                /*if (account == null)
                    return BadRequest("No such a account found to delete");
*/
                var result = accountService.Delete(id);

                if(result.Success)
                    return Ok(result);
                return BadRequest(result);
        }
    }
}
